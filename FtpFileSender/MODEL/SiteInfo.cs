namespace FtpFileSender.MODEL
{
    /// <summary>
    /// loggernet에 연결된 각 지점의 정보 관리 클래스
    /// </summary>
    public class SiteInfo
    {
        public string SiteName { get; set; }
        public string SiteCode { get; set; }
        public string LastestDate { get; set; }
        public string DataFile { get; set; }
    }
}
