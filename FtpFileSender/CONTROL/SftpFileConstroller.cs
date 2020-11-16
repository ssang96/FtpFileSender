using FtpFileSender.MODEL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpFileSender.CONTROL
{
    /// <summary>
    /// sftp로 데이터를 전송하는 디렉토리를 감시하는 클래스
    /// </summary>
    class SftpFileConstroller
    {
        /// <summary>
        /// loggernet에서 남기는 데이터 파일들의 이벤트를 감시하는 객체
        /// </summary>
        private FileSystemWatcher watcher = null;

        /// <summary>
        /// loggernet에서 남기는 데이터 파일들의 디렉토리 패스
        /// </summary>
        private string _directoryPath = @"C:\FloodControl\SendDataFile";

        /// <summary>
        /// 이벤트 로그 User Control에 이벤트 로그를 보여주기 위한 인터페이스
        /// </summary>
        private ILogDisplay _logDisplay;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="logDisplay"></param>
        public SftpFileConstroller(ILogDisplay logDisplay)
        {
            _logDisplay = logDisplay;
            this.watch();
        }

        /// <summary>
        /// sftp로 데이터를 전송하기 위해서 파일을 저장하는 디렉토리내의 파일이 신규로 생성되면 발생하는 이벤트
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void watch()
        {
            watcher = new FileSystemWatcher();

            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Path = _directoryPath;
            watcher.Filter = "*.*";
            watcher.Created += new FileSystemEventHandler(OnCreated);

            watcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            //파일명 얻어오기

            //접속하기

            //전송 후, 디렉토리 남아있는 데이터 호출

        }
    }
}
