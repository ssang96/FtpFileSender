namespace FtpFileSender.MODEL
{
    /// <summary>
    /// LoggerUserControl의 ListView에 로그를 표출하기 위한 MVC 의 인터페이스
    /// </summary>
    public interface ILogDisplay
    {
        void Display(string logs);
    }
}
