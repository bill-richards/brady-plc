using System.IO;
using System.Threading.Tasks;
using Brady.FileSystem;
using Brady.Serialization;
using NUnit.Framework;

namespace Brady
{
    [TestFixture]
    public class DirectoryWatcherAndDataProcessorIntegrationTests
    {
        private string ReportFileName = "01-Basic.xml";
        private string ReportSourceDirectoryName = "xml-docs";
        private IDirectoryWatcherFactory _watcherFactory;
        private string SourceFile => Path.Combine(TestHelper.GetCurrentDirectory(), ReportSourceDirectoryName, ReportFileName);

        private IDirectoryWatcherFactory WatcherFactory => _watcherFactory ??= new DirectoryWatcher();
        private IDirectoryWatcher TheWatcher { get; set; }

        [TearDown]
        public void TestTearDown()
        {
            if (TheWatcher != null)
            {
                TheWatcher.StopWatching();
                TheWatcher = null;
            }
            _watcherFactory = null;
        }

        [Test]
        public void WhenAFile_IsAddedToTheWatchedDirectory_TheDataProcessorCreatesTheExpected_GenerationOutput()
        {
            // Arrange
            var watchDirectory = Path.Combine(TestHelper.GetCurrentDirectory(), "watch-directory");
            var referenceDataFile = Path.Combine(TestHelper.GetCurrentDirectory(), "xml-docs", "ReferenceData.xml");
            var destinationFile = Path.Combine(watchDirectory, ReportFileName);
            Directory.CreateDirectory(watchDirectory);
            TheWatcher = WatcherFactory.CreateDirectoryWatcher(watchDirectory);
            TheWatcher.FileAddedToDirectory += async sourceFile =>
            {
                // Assert
                try
                {
                    var report = await DataProcessor.Process(sourceFile, referenceDataFile);
                    Assert.That(report, Is.Not.Null);
                    Assert.That(report, Is.InstanceOf<GenerationOutput>());
                }
                finally
                {
                    TestHelper.RemoveTestOutputFileAndDirectory(destinationFile, watchDirectory);
                }
            };
            TheWatcher.StartWatching();

            // Act
            File.Copy(SourceFile, destinationFile);

        }
    }
}