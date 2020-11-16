using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpFileSender.MODEL
{
    class FtpDirectoryInfo
    {
        public static string SFTPHOST { get; set; }
        public static string SFTPUSERNAME { get; set; }
        public static string SFTPPASSWORD { get; set; }
        public static string STFPPORT { get; set; }
        public static string LOCALDIRECTORYPATH { get; set; }
        public static string REMOTEDIRECTORYPATH { get; set; }
    }
}
