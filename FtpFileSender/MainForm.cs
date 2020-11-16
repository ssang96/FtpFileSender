using FtpFileSender.MODEL;
using FtpFileSender.VIEW;
using System.IO;
using System.Windows.Forms;

namespace FtpFileSender
{
    public partial class MainForm : Form
    {
        private readonly string _mainDirectory = @"C:\FloodControl";
        private LoginUserControl _loginUserControl;    
        private LoggerUserControl _loggerUserControl;
        private SiteManageUserControl _loginStatusUserControl;


        public MainForm()
        {
            InitializeComponent();

            this.InitDirectory();

            this.InitControl();
        }

        private void InitDirectory()
        {
            Directory.CreateDirectory(_mainDirectory);
        }

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
