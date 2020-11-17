using FtpFileSender.MODEL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FtpFileSender.VIEW
{
    public partial class LoggerUserControl : UserControl, ILogDisplay
    {
        public LoggerUserControl()
        {
            InitializeComponent();
        }

        public void Display(string logs)
        {
            bool result = true;

            if (this.lvCurrentStatus.Items.Count > 15)
                this.lvCurrentStatus.Items.Clear();

            var log = logs.Split(',');

            this.lvCurrentStatus.Items.Add(new ListViewItem(new[] { log[0], log[1] }));
        }
    }
}
