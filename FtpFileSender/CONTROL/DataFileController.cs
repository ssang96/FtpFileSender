using FtpFileSender.MODEL;
using System.IO;
using System.Linq;

namespace FtpFileSender.CONTROL
{
    /// <summary>
    /// loggernet에서 남기는 데이터 파일을 감시하는 클래스
    /// </summary>
    class DataFileController
    {
        /// <summary>
        /// loggernet에서 남기는 데이터 파일들의 이벤트를 감시하는 객체
        /// </summary>
        private System.IO.FileSystemWatcher watcher = null;

        /// <summary>
        /// loggernet에서 남기는 데이터 파일들의 디렉토리 패스
        /// </summary>
        private string _directoryPath = string.Empty;

        /// <summary>
        /// 이벤트 로그 User Control에 이벤트 로그를 보여주기 위한 인터페이스
        /// </summary>
        private ILogDisplay _logDisplay; 

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="logDisplay"></param>
        public DataFileController(ILogDisplay logDisplay)
        {
            _logDisplay = logDisplay;
        }

        /// <summary>
        /// 디렉토리내 파일 감시 환경 설정 메소드
        /// </summary>
        /// <param name="directoryPath"></param>
        public void watch(string directoryPath)
        {
            _directoryPath = directoryPath;

            if (watcher != null)
            {
                watcher.Path = Path.GetDirectoryName(directoryPath);
            }
            else
            {
                watcher = new FileSystemWatcher();
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName | NotifyFilters.DirectoryName;

                watcher.Path = Path.GetDirectoryName(directoryPath);
                watcher.Filter = Path.GetFileName(directoryPath);
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnChanged);

                watcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// loggernet에서 데이터를 저장하는 디렉토리내의 파일 변경이 발생하면 발생하는 이벤트
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var filePath = e.Name;
            var log = File.ReadLines(filePath).Reverse();

            //환경 파일에서 설정된 사이트의 파일인지 체크

            //맞다면, 마지막 체크한 datetime을 체크 

            //없다면, 가장 마지막 데이터만 생성

        }
    }
}
