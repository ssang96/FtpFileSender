using FtpFileSender.MODEL;
using log4net;
using Renci.SshNet;
using System;
using System.IO;

namespace FtpFileSender.CONTROL
{
    /// <summary>
    /// sftp로 데이터를 전송하는 디렉토리를 감시하는 클래스
    /// </summary>
    class SftpFileConstroller
    {
        /// <summary>
        /// log4net에 로그를 남기는 객체
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(SftpFileConstroller));

        /// <summary>
        /// loggernet에서 남기는 데이터 파일들의 이벤트를 감시하는 객체
        /// </summary>
        private FileSystemWatcher watcher = null;

        /// <summary>
        /// loggernet에서 남기는 데이터 파일들의 디렉토리 패스
        /// </summary>
        private string _directoryPath = @"C:\FloodControl\SendDataFile\";

        /// <summary>
        /// 이벤트 로그 User Control에 이벤트 로그를 보여주기 위한 인터페이스
        /// </summary>
        private ILogDisplay _logDisplay;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="logDisplay"></param>
        public SftpFileConstroller(ILogDisplay logDisplay)
        {
            _logDisplay = logDisplay;

            Directory.CreateDirectory(_directoryPath);
            this.watch();
        }

        /// <summary>
        /// sftp로 데이터를 전송하기 위해서 파일을 저장하는 디렉토리내의 파일이 신규로 생성되면 발생하는 이벤트
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void watch()
        {
            watcher = new FileSystemWatcher();

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Path = _directoryPath;
            watcher.Filter = "*.*";
            watcher.Created += new FileSystemEventHandler(OnCreated);

            watcher.EnableRaisingEvents = true;

            log.Info(_directoryPath + " monitoring");
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            //파일명 얻어오기
            var fullFileName = e.FullPath;
            var fileName = e.Name;

            //접속하기     
            try
            {
                using (var sftp = new SftpClient(FtpDirectoryInfo.SFTPHOST, int.Parse(FtpDirectoryInfo.STFPPORT), FtpDirectoryInfo.SFTPUSERNAME, FtpDirectoryInfo.SFTPPASSWORD))
                {
                    // SFTP 서버 연결
                    sftp.Connect();

                    _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + FtpDirectoryInfo.SFTPHOST + " 에 접속했습니다.");
                    log.Info(FtpDirectoryInfo.SFTPHOST + " connected");

                    sftp.ChangeDirectory(FtpDirectoryInfo.REMOTEDIRECTORYPATH);
                    _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + FtpDirectoryInfo.REMOTEDIRECTORYPATH + " 디렉토리를 변경했습니다.");
                    log.Info(FtpDirectoryInfo.REMOTEDIRECTORYPATH + " directory changed");
                    // SFTP 업로드
                    using (var infile = File.Open(fullFileName, FileMode.Open))
                    {
                        sftp.UploadFile(infile, fileName);
                        _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + fileName + " 업로드 했습니다.");
                        log.Info(fileName + " file upload");

                        File.Delete(fullFileName);
                        _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + fileName + " 삭제했습니다.");
                        log.Info(fileName + " deleted");
                    }

                    sftp.Disconnect();
                    _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + FtpDirectoryInfo.SFTPHOST + " 접속을 해제했습니다.");
                    log.Info(FtpDirectoryInfo.SFTPHOST + " disconnected");
                }
            }
            catch(Exception ex)
            {
                _logDisplay.Display(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + ex.Message + " 문제가 발생했습니다.");
                log.Error(ex.Message);
            }
            //전송 후, 디렉토리 남아있는 데이터 호출

        }
    }
}
