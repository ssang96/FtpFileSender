using FtpFileSender.CONTROL;
using FtpFileSender.MODEL;
using log4net;
using System;
using System.Windows.Forms;

namespace FtpFileSender.VIEW
{
    public partial class LoginUserControl : UserControl
    {
        /// <summary>
        /// log4net에 로그를 남기는 객체
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginUserControl));

        /// <summary>
        /// 로거넷에서 데이터를 남기는 디렉토리의 파일을 감시하는 클래스 
        /// </summary>
        private DataFileController _fileController;

        /// <summary>
        /// 이벤트 로그 User Control에 이벤트 로그를 보여주기 위한 인터페이스
        /// </summary>
        private ILogDisplay _logDisplay;

        public LoginUserControl(ILogDisplay logDispaly)
        {
            InitializeComponent();

            _logDisplay = logDispaly;

            InitEnvironment();
        }

        private void InitEnvironment()
        {
            this.txtHost.Text               = Properties.Settings.Default["HOSTIP"].ToString().Trim();
            this.txtPort.Text               = Properties.Settings.Default["HOSTPORT"].ToString().Trim();
            this.txtUserName.Text           = Properties.Settings.Default["USERNAME"].ToString().Trim();
            this.txtPassword.Text           = Properties.Settings.Default["PASSWORD"].ToString().Trim();
            this.txtLocalDirectory.Text     = Properties.Settings.Default["LOCALPATH"].ToString().Trim();
            this.txtRemoteDirectory.Text    = Properties.Settings.Default["REMOTEPATH"].ToString().Trim();           
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //empty check 
            if(this.txtHost.Text == "")
            {
                MessageBox.Show("SFTP 주소를 입력해 주세요");
            }

            if (this.txtPort.Text == "")
            {
                MessageBox.Show("SFTP 포트를 입력해 주세요");
            }

            if (this.txtUserName.Text == "")
            {
                MessageBox.Show("SFTP 접속 User Name을 입력해 주세요");
            }

            if (this.txtPassword.Text == "")
            {
                MessageBox.Show("SFTP 접속 패스워드를 입력해 주세요");
            }

            if (this.txtLocalDirectory.Text == "")
            {
                MessageBox.Show("Data 파일이 저장되는 Directory를 선택해 주세요");
            }

            //save config           
            Properties.Settings.Default["HOSTIP"]       = this.txtHost.Text.Trim();
            Properties.Settings.Default["HOSTPORT"]     = this.txtPort.Text.Trim();
            Properties.Settings.Default["USERNAME"]     = this.txtUserName.Text.Trim();
            Properties.Settings.Default["PASSWORD"]     = this.txtPassword.Text.Trim();
            Properties.Settings.Default["LOCALPATH"]    = this.txtLocalDirectory.Text.Trim();
            Properties.Settings.Default["REMOTEPATH"]   = this.txtRemoteDirectory.Text.Trim();

            //info object setting
            FtpDirectoryInfo.LOCALDIRECTORYPATH         = this.txtLocalDirectory.Text.Trim();
            FtpDirectoryInfo.REMOTEDIRECTORYPATH        = this.txtRemoteDirectory.Text.Trim();
            FtpDirectoryInfo.SFTPHOST                   = this.txtHost.Text.Trim();
            FtpDirectoryInfo.SFTPUSERNAME               = this.txtUserName.Text.Trim();
            FtpDirectoryInfo.LOCALDIRECTORYPATH         = this.txtLocalDirectory.Text.Trim();
            FtpDirectoryInfo.SFTPPASSWORD               = this.txtPassword.Text.Trim();

            //crete data file watch object create
            if(this._fileController != null)
            {
                this._fileController.watch(this.txtLocalDirectory.Text.Trim());
            }
            else
            {
                this._fileController = new DataFileController(this.txtLocalDirectory.Text.Trim());
            }
        }

        private void btnFileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Data Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "dat files (*.dat)|*.dat|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if(openFileDialog.FileName != null)
                {
                    txtLocalDirectory.Text = openFileDialog.FileName;
                   // _localPath = openFileDialog.FileName;
                }
            }
        }
    }
}
