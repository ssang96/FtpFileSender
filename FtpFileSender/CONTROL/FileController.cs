using System.IO;
using System.Linq;

namespace FtpFileSender.CONTROL
{
    class FileController
    {
        private FileSystemWatcher watcher;

        private string _directoryPath { get; set; } = @"C:\Datas";

        public FileController(string directoryPath)
        {
            _directoryPath = directoryPath;
            this.watch();
        }

        private void watch()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = _directoryPath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);  
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var orderedFiles = Directory.GetFiles(_directoryPath).Select(f => new FileInfo(f)).OrderBy(f => f.CreationTime);
        }
    }
}
