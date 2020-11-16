using FtpFileSender.MODEL;
using log4net;
using System;
using System.IO;
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
        private readonly string _directoryPath = @"C:\FloodControl\PointInfo";

        /// <summary>
        /// 이벤트 로그 User Control에 이벤트 로그를 보여주기 위한 인터페이스
        /// </summary>
        private ILogDisplay _logDisplay;
               
        public SiteManageUserControl(ILogDisplay logDisplay)
        {
            InitializeComponent();

            this._logDisplay = logDisplay;
            
            this.InitDirectory();
            
            this.InitLocationEnvironment();
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
                
            }
            //ListView Setting
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtCode.Text == "")
            {
                MessageBox.Show("관측소 코드를 입력해 주세요");
                return;
            }

            if(this.txtCode.Text == "")
            {
                MessageBox.Show("관측소명을 입력해 주세요");
                return;
            }              
        }
    }
}
