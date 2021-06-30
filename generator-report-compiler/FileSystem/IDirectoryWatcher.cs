using System;

namespace Brady.FileSystem
{
    public interface IDirectoryWatcher
    {
        event Action<string> FileAddedToDirectory;

        bool IsWatching { get; }
        void StartWatching();
        void StopWatching();
    }
}