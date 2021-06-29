using System;
using System.IO;

namespace Brady.FileSystem
{
    public class DirectoryWatcher : IDirectoryWatcher, IDirectoryWatcherFactory
    {
        public event Action<string> FileAddedToDirectory;

        private readonly FileSystemWatcher _watcher;

        public DirectoryWatcher() { }
        
        private DirectoryWatcher(string fullPath)
        {
            _watcher = new FileSystemWatcher
            {
                Path = fullPath,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.FileName,
                Filter = "*.xml",
                IncludeSubdirectories = false
            };
        }

        public IDirectoryWatcher CreateDirectoryWatcher(string fullPath) => new DirectoryWatcher(fullPath);

        public bool IsWatching { get; private set; } = false;

        public void StartWatching()
        {
            if (IsWatching) return;

            _watcher.Created += OnCreatedHandler;
            _watcher.EnableRaisingEvents = true;
            IsWatching = true;
        }

        public void StopWatching()
        {
            if (!IsWatching) return;

            _watcher.EnableRaisingEvents = false;
            _watcher.Created -= OnCreatedHandler;
            IsWatching = false;
        }

        private void OnCreatedHandler(object sender, FileSystemEventArgs e) 
            => OnFileAddedToDirectory(e.FullPath);

        protected virtual void OnFileAddedToDirectory(string filePath) 
            => FileAddedToDirectory?.Invoke(filePath);
    }
}