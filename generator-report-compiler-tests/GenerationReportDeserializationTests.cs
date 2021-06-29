using Brady.Serialization;
using NUnit.Framework;

namespace Brady
{
    public class GenerationReportDeserializationTests
    {
        private GenerationReport Report;

        [OneTimeSetUp]
        public void Setup() 
            => Report = GenerationReport.Deserialize("./xml-docs/01-Basic.xml");

        [Test]
        public void WhenTheObjectModelIsDeserialized_ThereShouldBe_2_WindGenerators()
        {
            // Arrange
            var expectedNumberOfGenerators = 2;
            
            // Assert
            Assert.That(Report.Wind.Count, Is.EqualTo(expectedNumberOfGenerators));
        }

        [Test]
        public void WhenTheObjectModelIsDeserialized_ThenWindGenerator_1_ShouldHave_3_DaysOfData()
        {
            // Arrange
            var expectedNumberOfDaysData = 3;

            // Assert
            Assert.That(Report.Wind[0].Days.Count, Is.EqualTo(expectedNumberOfDaysData));
        }

        [Test]
        public void WhenTheObjectModelIsDeserialized_ThenWindGenerator_2_ShouldHave_3_DaysOfData()
        {
            // Arrange
            var expectedNumberOfDaysData = 3;

            // Assert
            Assert.That(Report.Wind[1].Days.Count, Is.EqualTo(expectedNumberOfDaysData));
        }

        [Test]
        public void WhenTheObjectModelIsDeserialized_ThereShouldBe_1_GasGenerator()
        {
            // Arrange
            var expectedNumberOfGenerators = 1;

            // Assert
            Assert.That(Report.Gas.Count, Is.EqualTo(expectedNumberOfGenerators));
        }
        
        [Test]
        public void WhenTheObjectModelIsDeserialized_ThenTheGasGenerator_ShouldHaveTheExpectedName()
        {
            // Arrange
            var expectedName = "Gas[1]";

            // Assert
            Assert.That(Report.Gas[0].Name, Is.EqualTo(expectedName));
        }
        
        [Test]
        public void WhenTheObjectModelIsDeserialized_ThenTheGasGenerator_ShouldHaveTheExpectedEmissionsRating()
        {
            // Arrange
            var expectedEmissionsRating = 0.038D;

            // Assert
            Assert.That(Report.Gas[0].EmissionsRating, Is.EqualTo(expectedEmissionsRating));
        }

        [Test]
        public void WhenTheObjectModelIsDeserialized_ThereShouldBe_1_CoalGenerator()
        {
            // Arrange
            var expectedNumberOfGenerators = 1;
            // Assert
            Assert.That(Report.Coal.Count, Is.EqualTo(expectedNumberOfGenerators));
        }

        [TestCase(1,10.146)]
        [TestCase(2,11.815)]
        [TestCase(3,11.815)]
        public void WhenTheObjectModelIsDeserialized_ThenTheCoalGenerator_ShouldHaveTheExpectedPriceForEachDay(int dayNumber, double expectedPrice)
        {
            // Assert
            Assert.That(Report.Coal[0].Days[dayNumber-1].Price, Is.EqualTo(expectedPrice));
        }
    }
}