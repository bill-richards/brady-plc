using System;
using System.IO;
using Brady.FileSystem;

namespace Brady
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = AppConfiguration.GetConfiguration();
            var message = string.Empty;

            if (config.InputDirectory.IsNullOrWhitespace())
                message += "InputDirectory is not specified in appsettings.json\n";
            if (!config.InputDirectory.IsNullOrWhitespace() && !Directory.Exists(config.InputDirectory))
                message += $"InputDirectory [{config.InputDirectory}] specified in appsettings.json does not exist\n";


            if (config.OutputDirectory.IsNullOrWhitespace())
                message += "OutputDirectory is not specified in appsettings.json\n";
            if (!config.OutputDirectory.IsNullOrWhitespace() && !Directory.Exists(config.OutputDirectory))
                message += $"OutputDirectory [{config.OutputDirectory}] specified in appsettings.json does not exist\n";


            if (config.ReferenceDataFilePath.IsNullOrWhitespace())
                message += "ReferenceDataFilePath is not specified in appsettings.json\n";
            if (!config.ReferenceDataFilePath.IsNullOrWhitespace() && !File.Exists(config.ReferenceDataFilePath) )
                message += $"Using ReferenceDataFilePath [{config.ReferenceDataFilePath}] the file does not exist\n";

            Console.Write(message);
            Console.WriteLine("Exit to close");

            ProgramExecution program = null;
            if (message.IsNullOrWhitespace())
            {
                program = new ProgramExecution(config);
            }

            var entry = Console.ReadLine();
            while (entry.ToUpper() != "EXIT")
            {
                entry = Console.ReadLine();
            }

            program?.StopWatching();
        }
    }

    class ProgramExecution
    {
        private IDirectoryWatcherFactory _watcherFactory;
        private IDirectoryWatcherFactory WatcherFactory => _watcherFactory ??= new DirectoryWatcher();
        private IDirectoryWatcher TheWatcher { get; set; }

        public ProgramExecution(AppConfiguration config)
        {
            TheWatcher = WatcherFactory.CreateDirectoryWatcher(config.InputDirectory);
            TheWatcher.FileAddedToDirectory += async sourceFile =>
            {
                var report = await DataProcessor.Process(sourceFile, config.ReferenceDataFilePath);
                var filename = Path.GetFileNameWithoutExtension(sourceFile);
                


                var fileName = Path.Combine(config.OutputDirectory, $"{Path.GetFileNameWithoutExtension(sourceFile)}-Result.xml");
                report.SerializeToFile(fileName);
                File.Delete(sourceFile);
            };
            TheWatcher.StartWatching();
        }

        public void StopWatching()
        {
            TheWatcher.StopWatching();
        }
    }
}
