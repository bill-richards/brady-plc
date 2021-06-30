using System.IO;
using System.Reflection;
using Brady.FileSystem;
using NUnit.Framework;

namespace Brady
{
    [TestFixture]
    public class DirectoryWatcherTests
    {
        private IDirectoryWatcherFactory _watcherFactory;

        private IDirectoryWatcherFactory WatcherFactory => _watcherFactory ??= new DirectoryWatcher();
        private IDirectoryWatcher TheWatcher { get; set; }

        [TearDown]
        public void TestTearDown()
        {
            if(TheWatcher != null)
            {
                TheWatcher.StopWatching();
                TheWatcher = null;
            }
            _watcherFactory = null;
        }

        [Test]
        public void WhenTheWatcherIsCreated_TheProperty_IsWatching_ShouldReturnFalse()
        {
            // Arrange
            var workingDirectory = TestHelper.GetCurrentDirectory();

            // Act
            TheWatcher = WatcherFactory.CreateDirectoryWatcher(workingDirectory);

            // Assert
            Assert.That(TheWatcher.IsWatching, Is.False);
        }

        [Test]
        public void WhenTheWatcherHasStarted_TheProperty_IsWatching_ShouldReturnTrue()
        {
            // Arrange
            var workingDirectory = TestHelper.GetCurrentDirectory();
            TheWatcher = WatcherFactory.CreateDirectoryWatcher(workingDirectory);

            // Act
            TheWatcher.StartWatching();

            // Assert
            Assert.That(TheWatcher.IsWatching, Is.True);
        }

        [Test]
        public void WhenTheWatcherHasStarted_AndNoFileIsAdded_TheEvent_FileAddedToDirectory_ShouldNotBeRaised()
        {
            // Arrange
            var eventRaised = false;
            var workingDirectory = TestHelper.GetCurrentDirectory();
            TheWatcher = WatcherFactory.CreateDirectoryWatcher(workingDirectory);
            TheWatcher.FileAddedToDirectory += path => eventRaised = true;

            // Act
            TheWatcher.StartWatching();

            // Assert
            Assert.That(eventRaised, Is.False);
        }

        [Test]
        public void WhenTheWatcherHasStarted_AndAFileIsAdded_TheEvent_FileAddedToDirectory_ShouldBeRaised()
        {
            var eventRaised = false;
            var root = TestHelper.GetCurrentDirectory();

            var fileName = "01-Basic.xml";
            var destination = Path.Combine(root, "EventRaised");
            var sourcePath = Path.Combine(root, @"xml-docs");
            var sourceFile = Path.Combine(sourcePath, fileName);
            var destinationFile = Path.Combine(destination, fileName);

            try
            {
                // Arrange
                Directory.CreateDirectory(destination);

                TheWatcher = WatcherFactory.CreateDirectoryWatcher(destination);
                TheWatcher.FileAddedToDirectory += path => eventRaised = true;
                TheWatcher.StartWatching();

                // Act
                File.Copy(sourceFile, destinationFile);

                // Assert
                Assert.That(eventRaised, Is.True);
            }
            finally
            {
                TestHelper.RemoveTestOutputFileAndDirectory(destinationFile, destination);
            }
        }

        [Test]
        public void WhenTheWatcherHasStarted_AndAFileIsAdded_TheEvent_FileAddedToDirectory_ShouldIndicateTheNewlyAddedFile()
        {
            var addedFileName = string.Empty;
            var root = TestHelper.GetCurrentDirectory();

            var fileName = "01-Basic.xml";
            var destination = Path.Combine(root, "IndicateFileName");
            var sourcePath = Path.Combine(root, @"xml-docs");
            var sourceFile = Path.Combine(sourcePath, fileName);
            var destinationFile = Path.Combine(destination, fileName);

            try
            {
                // Arrange
                Directory.CreateDirectory(destination);

                TheWatcher = WatcherFactory.CreateDirectoryWatcher(destination);
                TheWatcher.FileAddedToDirectory += path => addedFileName = path;
                TheWatcher.StartWatching();

                // Act
                File.Copy(sourceFile, destinationFile);

                // Assert
                Assert.That(addedFileName, Is.EqualTo(destinationFile));
            }
            finally
            {
                TestHelper.RemoveTestOutputFileAndDirectory(destinationFile, destination);
            }
        }

    }
}