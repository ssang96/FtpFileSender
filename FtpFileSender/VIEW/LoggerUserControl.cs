using FtpFileSender.MODEL;
using System;
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

            Console.WriteLine(logs);
        }
    }
}
