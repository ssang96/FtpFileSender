using FtpFileSender.CONTROL;
using FtpFileSender.MODEL;
using log4net;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FtpFileSender.VIEW
{
    public partial class SiteManageUserControl : UserControl
    {
        /// <summary>
        /// log4net에 로그를 남기는 객체
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(SiteManageUserControl));

        /// <summary>
        /// 사이트 정보를 관리하는 디렉토리
        /// </summary>
        private readonly string _directoryPath = @"C:\FloodControl\PointInfo\";

        /// <summary>
        /// 이벤트 로그 User Control에 이벤트 로그를 보여주기 위한 인터페이스
        /// </summary>
        private ILogDisplay _logDisplay;

        private SftpFileConstroller _sftpFileController;

        private string _deleteFileName = string.Empty;
      
               
        public SiteManageUserControl(ILogDisplay logDisplay)
        {
            InitializeComponent();

            this._logDisplay = logDisplay;
            
            this.InitDirectory();
            
            this.InitLocationEnvironment();

            _sftpFileController = new SftpFileConstroller(logDisplay);
        }

        private void InitDirectory()
        {
            Directory.CreateDirectory(_directoryPath);            
        }

        private void InitLocationEnvironment()
        {
            var files = Directory.GetFiles(this._directoryPath);          

            //Files Check
            if (files.Length > 0)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    var fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        var line = streamReader.ReadToEnd();

                        string[] datas = line.Split(',');
                       
                        SiteInfo siteInfo = new SiteInfo();
                        siteInfo.SiteName = datas[0].Trim();
                        siteInfo.SiteCode = datas[1].Trim();
                        siteInfo.DataFile = datas[2].Trim();

                        if (datas[3] != "\r\n")
                            siteInfo.LastestDate = datas[3].Replace("\"", "");

                        SitesInfoList.AddInfo(siteInfo.SiteName, siteInfo.SiteCode, siteInfo.DataFile, siteInfo.LastestDate);
                        //리스트 뷰 추가
                        this.lvSites.Items.Add(new ListViewItem(new[] { "", siteInfo.SiteName, siteInfo.SiteCode, siteInfo.DataFile}));
                      
                    }
                }
            }
            //ListView Setting
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtLocationName.Text == "")
            {
                MessageBox.Show("관측소명을 입력해 주세요");
                return;
            }

            if(this.txtCode.Text == "")
            {
                MessageBox.Show("관측소 코드를 입력해 주세요");
                return;
            }

            //사이트명으로 존재 여부 확인
            bool exist = SitesInfoList.CheckExisWithSiteCodet(this.txtCode.Text.Trim());

            //배열에 추가 
            if (exist)
            {
                MessageBox.Show("동일한 이름의 사이트가 존재합니다.");
                return;
            }             
           
            //파일 추가
            CreateNewSiteInfo(this.txtLocationName.Text.Trim(), this.txtCode.Text.Trim());
        }

        private bool CreateNewSiteInfo(string siteName, string siteCode)
        {
            var result = true;

            try
            {
                using (StreamWriter sw = File.CreateText(_directoryPath + siteName + "_Flux"))
                {
                    sw.WriteLine(siteName + "," + siteCode + "," + siteName + "_Flux" + "," + null );

                    //리스트 뷰 추가
                    this.lvSites.Items.Add(new ListViewItem(new[] { "", siteName, siteCode, siteName + "_Flux" }));

                    SitesInfoList.AddInfo(siteName, siteCode, siteName + "_Flux", null);

                }

                using (StreamWriter sw = File.CreateText(_directoryPath + siteName + "_Slow"))
                {
                    sw.WriteLine(siteName + "," + siteCode + "," + siteName + "_Slow" + "," + null);

                    //리스트 뷰 추가
                    this.lvSites.Items.Add(new ListViewItem(new[] { "", siteName, siteCode, siteName + "_Slow" }));

                    SitesInfoList.AddInfo(siteName, siteCode, siteName + "_Slow", null);
                }
            }
            catch (Exception Ex)
            {
                result = false;
                Console.WriteLine(Ex.ToString());
            }
            return result;
        }

        private bool RemoveSiteInfo(string siteName, string fileName)
        {
            var result = true;

            try
            {
                if(fileName.Contains("Flux"))
                    File.Delete(_directoryPath + Site + "_Flux");
                else
                    File.Delete(_directoryPath + Site + "_Slow");
            }
            catch(Exception ex)
            {
                result = false;
                Console.WriteLine(ex.ToString());
            }

            return result;
        }

        private void lvSites_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.lvSites.SelectedItems.Count == 0)
                return;
           
            for (int i = 0; i < this.lvSites.Items.Count; i++)
            {
                this.lvSites.Items[i].ForeColor = Color.Black;
            }
            
            this.lvSites.SelectedItems[0].ForeColor = Color.Blue;          

            ListViewItem items = this.lvSites.SelectedItems[0];

            this.txtLocationName.Text   = items.SubItems[1].Text.ToString();
            this.txtCode.Text           = items.SubItems[2].Text.ToString();
            this._deleteFileName        = items.SubItems[3].Text.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text == "")
            {
                MessageBox.Show("테이블에서 관측소를 클릭해 주세요");
                return;
            }

            if (this.txtCode.Text == "")
            {
                MessageBox.Show("테이블에서 관측소를 클릭해 주세요");
                return;
            }

            var result = SitesInfoList.RemoveInfo(this.txtLocationName.Text, _deleteFileName);

            //파일 삭제

            //성공이면 다시 그린다.
            if(result)
            {
                this.lvSites.Items.Clear();

                Hashtable sitesInfo = SitesInfoList.GetSitesInfoList();

                if(sitesInfo.Count > 0)
                {
                    foreach (SiteInfo siteInfo in sitesInfo.Values)
                    {
                        //리스트 뷰 추가
                        this.lvSites.Items.Add(new ListViewItem(new[] { "", siteInfo.SiteName, siteInfo.SiteCode, siteInfo.DataFile}));

                    }
                }
            }
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자만 입력되도록 필터링
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    
            {
                e.Handled = true;
            }
        }
    }
}
