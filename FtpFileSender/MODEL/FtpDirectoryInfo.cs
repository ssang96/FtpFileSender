namespace FtpFileSender.MODEL
{
    /// <summary>
    /// sftp 정보를 저장하는 sftp 정보 저장 클래스
    /// </summary>
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
