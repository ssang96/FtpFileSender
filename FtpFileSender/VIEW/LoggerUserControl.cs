using FtpFileSender.MODEL;
using log4net;
using System;
using System.Windows.Forms;

namespace FtpFileSender.VIEW
{
    public partial class LoggerUserControl : UserControl, ILogDisplay
    {
        /// <summary>
        /// log4net에 로그를 남기는 객체
        /// </summary>
        private static readonly ILog logger= LogManager.GetLogger(typeof(LoggerUserControl));

        public LoggerUserControl()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;            
        }

        public void Display(string logs)
        {
            bool result = true;

            try
            {
                if (this.lvCurrentStatus.Items.Count > 13)
                {
                    this.lvCurrentStatus.Items.Clear();                   
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
            
            var log = logs.Split(',');
            this.lvCurrentStatus.Items.Add(new ListViewItem(new[] { log[0], log[1] }));
        }
    }
}
