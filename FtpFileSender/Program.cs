using System;
using System.Threading;
using System.Windows.Forms;

namespace FtpFileSender
{
    static class Program
    {
        private static Mutex mutex = null;

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string appName = "FtpFileSender";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {                 
                MessageBox.Show("FtpFileSender Program Already Running!");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
