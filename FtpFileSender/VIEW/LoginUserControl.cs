using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FtpFileSender.VIEW
{
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
           
        }

        private void btnFileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

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

                    testread(openFileDialog.FileName);
                }
            }
        }

        private void testread(string path)
        {
            var lines = File.ReadLines(path).Reverse();

            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
