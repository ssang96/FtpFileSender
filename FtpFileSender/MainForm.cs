using FtpFileSender.CONTROL;
using FtpFileSender.VIEW;
using System.Windows.Forms;

namespace FtpFileSender
{
    public partial class MainForm : Form
    {        
        private LoginUserControl _loginUserControl;    
        private LoggerUserControl _loggerUserControl;
        private LoginStatusUserControl _loginStatusUserControl;

        public MainForm()
        {
            InitializeComponent();

            _loginUserControl           = new LoginUserControl();           
            _loggerUserControl          = new LoggerUserControl();
            _loginStatusUserControl     = new LoginStatusUserControl();

            this.pnlLogin.Controls.Add(_loginUserControl);
            this.pnlLoginStatus.Controls.Add(_loginStatusUserControl);
            this.pnlCurrentStatus.Controls.Add(_loggerUserControl);
        }
    }
}
