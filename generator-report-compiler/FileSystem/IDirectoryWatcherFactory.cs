namespace Brady.FileSystem
{
    public interface IDirectoryWatcherFactory
    {
        IDirectoryWatcher CreateDirectoryWatcher(string fullPath);
    }
}