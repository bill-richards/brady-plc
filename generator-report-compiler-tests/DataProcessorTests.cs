using System.IO;
using System.Threading.Tasks;
using Brady.Serialization;
using NUnit.Framework;

namespace Brady
{
    [TestFixture]
    public class DataProcessorTests
    {
        private string _referenceDataFile;

        public string ReferenceDataFile
        {
            get
            {
                if (_referenceDataFile.IsNullOrWhitespace()) 
                    _referenceDataFile = Path.Combine(TestHelper.GetCurrentDirectory(), "xml-docs", "ReferenceData.xml");

                return _referenceDataFile;
            }
        }

        [Test]
        public void WhenTheStaticMember_Process_IsCalledForNonExistentFiles_TheTask_ShouldReturnNull()
        {
            // Arrange
            var fileName = Path.Combine(TestHelper.GetCurrentDirectory(), "imaginary_file.txt");

            // Act
            var task = DataProcessor.Process(fileName, ReferenceDataFile);
            task.Wait();

            // Assert
            Assert.That(task.Result, Is.Null);
        }

        [Test]
        public async Task WhenTheStaticMember_Process_IsCalledForExistingFiles_TheTask_ShouldReturn_A_GenerationOutput()
        {
            // Arrange
            var fileName = Path.Combine(TestHelper.GetCurrentDirectory(), "xml-docs", "01-Basic.xml");

            // Act
            var report = await DataProcessor.Process(fileName, ReferenceDataFile);

            // Assert
            Assert.That(report, Is.InstanceOf<GenerationOutput>());
        }

        [Test]
        public async Task WhenTheStaticMember_Process_IsCalledForExistingFiles_TheTask_ShouldReturn_TheExpectedNumberOf_GenerationOutputs()
        {
            // Arrange
            var fileName = Path.Combine(TestHelper.GetCurrentDirectory(), "xml-docs", "01-Basic.xml");

            // Act
            var output = await DataProcessor.Process(fileName, ReferenceDataFile);

            // Assert
            Assert.That(output.GeneratorOutputs.Count, Is.EqualTo(4));
        }

        [Test]
        public async Task WhenTheStaticMember_Process_IsCalledForExistingFiles_TheTask_ShouldReturn_TheExpectedNumberOf_EmissionsOutputs()
        {
            // Arrange
            var fileName = Path.Combine(TestHelper.GetCurrentDirectory(), "xml-docs", "01-Basic.xml");

            // Act
            var output = await DataProcessor.Process(fileName, ReferenceDataFile);

            // Assert
            Assert.That(output.EmissionsOutputs.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task WhenTheStaticMember_Process_IsCalledForExistingFiles_TheTask_ShouldReturn_TheExpectedNumberOf_ActualHeatRates()
        {
            // Arrange
            var fileName = Path.Combine(TestHelper.GetCurrentDirectory(), "xml-docs", "01-Basic.xml");

            // Act
            var output = await DataProcessor.Process(fileName, ReferenceDataFile);

            // Assert
            Assert.That(output.HeatRates.Count, Is.EqualTo(1));
        }
    }
}