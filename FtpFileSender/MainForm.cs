using FtpFileSender.VIEW;
using System.IO;
using System.Windows.Forms;

namespace FtpFileSender
{
    /// <summary>
    /// MainForm 클래스   
    /// </summary>
    public partial class MainForm : Form
    {
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
    }
}
