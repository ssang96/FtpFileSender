using FtpFileSender.MODEL;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace FtpFileSender.CONTROL
{
    /// <summary>
    /// loggernet에서 남기는 데이터 파일을 감시하는 클래스
    /// 가. 홍천: 1014801
    /// 나. 인제: 1011801
    /// 다. 평창: 1001801
    /// 라. 철원: 1022801
    /// 마. 제천: 1003801
    /// </summary>
    class DataFileController
    {
        /// <summary>
        /// log4net에 로그를 남기는 객체
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(DataFileController));

        /// <summary>
        /// loggernet에서 남기는 데이터 파일들의 이벤트를 감시하는 객체
        /// </summary>
        private System.IO.FileSystemWatcher watcher = null;

        /// <summary>
        /// 사이트 정보를 관리하는 디렉토리
        /// </summary>
        private string _directoryPath = @"C:\FloodControl\PointInfo\";

        /// <summary>
        /// loggernet에서 남기는 데이터 파일들의 디렉토리 패스
        /// </summary>
        private string _sftpDirectoryPath = @"C:\FloodControl\SendDataFile\";

        /// <summary>
        /// 이벤트 로그 User Control에 이벤트 로그를 보여주기 위한 인터페이스
        /// </summary>
        private ILogDisplay _logDisplay;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="logDisplay"></param>
        public DataFileController(ILogDisplay logDisplay)
        {
            _logDisplay = logDisplay;
        }

        /// <summary>
        /// 디렉토리내 파일 감시 환경 설정 메소드
        /// </summary>
        /// <param name="directoryPath"></param>
        public void watch(string directoryPath)
        {
            if (watcher != null)
            {
                watcher.Path = Path.GetDirectoryName(directoryPath);
            }
            else
            {
                watcher = new FileSystemWatcher();
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName;

                watcher.Path = directoryPath;
                watcher.Filter = "*.dat";
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnChanged);

                watcher.EnableRaisingEvents = true;               
            }
        }

        public void Close()
        {
            if (this.watcher != null)
                this.watcher.Dispose();
        }

        /// <summary>
        /// loggernet에서 데이터를 저장하는 디렉토리내의 파일 변경이 발생하면 발생하는 이벤트
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {            
            var filePath = e.FullPath;
            var siteName = e.Name.Split('_');
            var exist = false;
            string siteInfoFileName = string.Empty;

            if (e.Name.ToUpper().Contains("FLUX") && !e.Name.ToUpper().Contains("NOTES"))
                siteInfoFileName = siteName[0] + "_Flux";
            else if (e.Name.ToUpper().Contains("30M"))
                siteInfoFileName = siteName[0] + "_Slow";
            else
                return;
           
            exist = SitesInfoList.CheckExistWithSiteName(siteInfoFileName);

            //환경 파일에서 설정된 사이트의 파일인지 체크
            try
            {
                if (exist)
                {
                    Thread.Sleep(1);
                    var AllLines = File.ReadAllLines(filePath).Reverse();
                    var siteInfo = SitesInfoList.GetSiteInfo(siteInfoFileName);

                    //마지막 저장 date가 있다면 체크해서 파일 생성
                    if (siteInfo.LastestDate != null)
                    {
                        List<FileDataStructure> fileDataList = new List<FileDataStructure>();

                        foreach (string data in AllLines)
                        {
                            var date = data.Split(',')[0].Replace("\"", "");

                            DateTime dates = DateTime.Parse(date);

                            if (date != "" && DateTime.Parse(siteInfo.LastestDate) < dates)
                            {
                                FileDataStructure dataStructure = new FileDataStructure();
                                dataStructure.dates = Convert.ToDateTime(date);
                                dataStructure.rowData = data;

                                //flux                             
                                if (e.Name.Contains("Flux"))
                                {
                                    dataStructure.type = "Flux";
                                }
                                else //slow
                                {
                                    dataStructure.type = "30m";
                                }
                                fileDataList.Add(dataStructure);
                            }
                            else
                                break;
                        }

                        if (fileDataList.Count > 0)
                        {
                            MakeFile(fileDataList, e.Name, siteInfo);
                        }
                    }
                    else  //없다면, 가장 마지막 데이터만 생성
                    {
                        foreach (string data in AllLines)
                        {
                            if (data != "")
                            {
                                FileDataStructure dataStructure = new FileDataStructure();
                                dataStructure.dates = Convert.ToDateTime(data.Split(',')[0].Replace("\"", ""));
                                dataStructure.rowData = data;

                                if (e.Name.Contains("Flux"))
                                {
                                    dataStructure.type = "Flux";
                                }
                                else //slow
                                {
                                    dataStructure.type = "30m";
                                }
                                this.MakeFile(dataStructure, e.Name, siteInfo);
                            }
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
        }

        /// <summary>
        /// 다수이 데이터 파일을 생성하는 메소드
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileName"></param>
        /// <param name="siteInfo"></param>
        private void MakeFile(List<FileDataStructure> datas, string fileName, SiteInfo siteInfo)
        {
            //1001801_년월일시분_Flux.dat, 1001801_년월일시분_30m.dat
            string ftpFileName = string.Empty;

            List<FileDataStructure> list = datas.OrderBy(data => data.dates).ToList();

            try
            {
                foreach (FileDataStructure data in list)
                {
                    //1001801_년월일시분_Flux.dat, 1001801_년월일시분_30m.dat
                    ftpFileName = _sftpDirectoryPath + siteInfo.SiteCode + "_" + data.dates.ToString("yyyyMMddHHmm") + "_" + data.type + ".dat";

                    using (StreamWriter writer = new StreamWriter(ftpFileName, false))
                    {
                        writer.Write(data.rowData);

                        _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + ftpFileName + " 을 생성했습니다.");
                        log.Info(ftpFileName + " created");
                    }

                    Thread.Sleep(1);

                    using (StreamWriter writer = new StreamWriter(_directoryPath + siteInfo.DataFile, false))
                    {
                        writer.Write(siteInfo.SiteName + "," + siteInfo.SiteCode + "," + siteInfo.DataFile + "," + data.dates.ToString("yyyy-MM-dd HH:mm:ss"));
                        log.Info(siteInfo.SiteName + "," + siteInfo.SiteCode + "," + siteInfo.DataFile + "," + data.dates.ToString("yyyy-MM-dd HH:mm:ss") + " updated");
                    }
                    SitesInfoList.UpdateLastedDate(siteInfo.DataFile, data.dates);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + ex.Message + " 문제가 발생했습니다.");
            }

        }

        /// <summary>
        /// 하나의 데이터 파일을 생성하는 메소드
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileName"></param>
        /// <param name="siteInfo"></param>
        private void MakeFile(FileDataStructure datas, string fileName, SiteInfo siteInfo)
        {
            string ftpFileName = string.Empty;

            try
            {
                //1001801_년월일시분_Flux.dat, 1001801_년월일시분_30m.dat
                ftpFileName = _sftpDirectoryPath + siteInfo.SiteCode + "_" + datas.dates.ToString("yyyyMMddHHmm") + "_" + datas.type + ".dat";
                
                using (StreamWriter writer = new StreamWriter(ftpFileName, false))
                {
                    writer.Write(datas.rowData);

                    _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + ftpFileName + " 을 생성했습니다.");
                    log.Info(ftpFileName + " created");
                }

                using (StreamWriter writer = new StreamWriter(_directoryPath + siteInfo.DataFile, false)) 
                {
                    writer.Write(siteInfo.SiteName + "," + siteInfo.SiteCode + "," + siteInfo.DataFile + "," + datas.dates.ToString("yyyy-MM-dd HH:mm:ss"));
                    log.Info(siteInfo.SiteName + "," + siteInfo.SiteCode + "," + siteInfo.DataFile + "," + datas.dates.ToString("yyyy-MM-dd HH:mm:ss") + " updated");
                }

                SitesInfoList.UpdateLastedDate(siteInfo.DataFile, datas.dates);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + ex.Message + " 문제가 발생했습니다.");
            }
        }
    }
}
