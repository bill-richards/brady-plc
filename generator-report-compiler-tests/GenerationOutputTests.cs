using Brady.Serialization;
using NUnit.Framework;
using System.Xml.Serialization;
using System.IO;

namespace Brady
{
    [TestFixture]
    public class GenerationOutputTests
    {
        private string OutputFileName = "xml-out.xml";

        private static GenerationOutput DeserializeFromFile(string fullPath)
        {
            var serializer = new XmlSerializer(typeof(GenerationOutput));
            using Stream reader = new FileStream(fullPath, FileMode.Open);
            return (GenerationOutput)serializer.Deserialize(reader);
        }

        [OneTimeSetUp]
        public void FixtureSetup() => TestDataForOutput.CreateGenerationOutput();
        

        [Test]
        public void WhenAGenerationOutputIsSerializedToFile_DeserializationResultsInTheExpected_GenerationOutput()
        {
            // Arrange
            var outputDirectory = Path.Combine(TestHelper.GetCurrentDirectory(), "expected-results");
            var outputFile = Path.Combine(outputDirectory, OutputFileName);
            var testData = TestDataForOutput.Output;
            Directory.CreateDirectory(outputDirectory);

            // Act
            testData.SerializeToFile(outputFile);
            var deserialized = DeserializeFromFile(outputFile);

            // Assert
            Assert.That(deserialized.DayOutputs.Count, Is.EqualTo(testData.DayOutputs.Count));
            Assert.That(deserialized.DayOutputs.Count, Is.EqualTo(3));
            Assert.That(deserialized.DayOutputs[1].Emission, Is.EqualTo(136.440767624));
            Assert.That(deserialized.GeneratorOutputs.Count, Is.EqualTo(testData.GeneratorOutputs.Count));
            Assert.That(deserialized.GeneratorOutputs.Count, Is.EqualTo(4));
            Assert.That(deserialized.GeneratorOutputs[3].Total, Is.EqualTo(5341.716526632));
            Assert.That(deserialized.HeatRates.Count, Is.EqualTo(testData.HeatRates.Count));
            Assert.That(deserialized.HeatRates.Count, Is.EqualTo(1));

            TestHelper.RemoveTestOutputFileAndDirectory(outputFile, outputDirectory);
        }
    }
}