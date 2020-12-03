using FtpFileSender.VIEW;
using log4net;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FtpFileSender
{
    /// <summary>
    /// MainForm 클래스   
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// log4net에 로그를 남기는 객체
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(MainForm));

        private readonly string _mainDirectory = @"C:\FloodControl";
        private LoginUserControl _loginUserControl;    
        private LoggerUserControl _loggerUserControl;
        private SiteManageUserControl _loginStatusUserControl;

        public MainForm()
        {
            InitializeComponent();

            this.MaximizeBox = false;

            this.InitDirectory();

            this.InitControl();

            Thread.Sleep(10);
            _loggerUserControl.Display(null);
        }
        /// <summary>
        /// 최초 실행시 root 디렉토리 생성
        /// </summary>
        private void InitDirectory()
        {
            Directory.CreateDirectory(_mainDirectory);
        }

        /// <summary>
        /// User Control 생성
        /// </summary>
        private void InitControl()
        {            
            _loggerUserControl      = new LoggerUserControl();
            _loginUserControl       = new LoginUserControl(_loggerUserControl);
            _loginStatusUserControl = new SiteManageUserControl(_loggerUserControl);

            this.pnlLogin.Controls.Add(_loginUserControl);
            this.pnlSiteManage.Controls.Add(_loginStatusUserControl);
            this.pnlCurrentStatus.Controls.Add(_loggerUserControl);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            log.Info("Clsoed sftp program");
        }
    }
}
