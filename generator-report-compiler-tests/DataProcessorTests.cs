using System.IO;
using System.Threading.Tasks;
using Brady.FileSystem;
using Brady.Serialization;
using NUnit.Framework;

namespace Brady
{
    [TestFixture]
    public class DataProcessorTests
    {
        [Test]
        public void WhenTheStaticMember_Process_IsCalledForNonExistentFiles_TheTask_ShouldReturnNull()
        {
            // Arrange
            var fileName = Path.Combine(TestHelper.GetCurrentDirectory(), "imaginary_file.txt");

            // Act
            var task = DataProcessor.Process(fileName);
            task.Wait();

            // Assert
            Assert.That(task.Result, Is.Null);
        }

        [Test]
        public async Task WhenTheStaticMember_Process_IsCalledForExistingFiles_TheTask_ShouldReturn_A_GenerationReport()
        {
            // Arrange
            var fileName = Path.Combine(TestHelper.GetCurrentDirectory(), "xml-docs", "01-Basic.xml");

            // Act
            var report = await DataProcessor.Process(fileName);

            // Assert
            Assert.That(report, Is.InstanceOf<GenerationReport>());
        }

        [Test]
        public async Task WhenTheStaticMember_Process_IsCalledForExistingFiles_TheTask_ShouldReturn_TheExpected_GenerationReport()
        {
            // Arrange
            var fileName = Path.Combine(TestHelper.GetCurrentDirectory(), "xml-docs", "01-Basic.xml");

            // Act
            var report = await DataProcessor.Process(fileName);

            // Assert
            Assert.That(report.Wind[1].Location, Is.EqualTo("Onshore"));
        }

    }
}