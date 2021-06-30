using System.IO;
using Brady.Serialization;
using NUnit.Framework;

namespace Brady
{
    [TestFixture]
    public class ReferenceDataTests
    {
        private ReferenceData _referenceData;

        [OneTimeSetUp]
        public void Setup()
        {
            var filePath = Path.Combine(TestHelper.GetCurrentDirectory(), "xml-docs", "ReferenceData.xml");
            _referenceData = ReferenceData.DeserializeFromFile(filePath);
        }

        [Test]
        public void WhenTheObjectModelIsDeserialized_TheValueFactorProperties_ShouldBe_TheExpectedValues()
        {
            // Arrange
            double high = 0.946, mid = 0.696, low = 0.265;

            // Assert
            Assert.That(_referenceData.ValueFactor.High, Is.EqualTo(high));
            Assert.That(_referenceData.ValueFactor.Medium, Is.EqualTo(mid));
            Assert.That(_referenceData.ValueFactor.Low, Is.EqualTo(low));
        }

        [Test]
        public void WhenTheObjectModelIsDeserialized_TheEmissionsFactorProperties_ShouldBe_TheExpectedValues()
        {
            // Arrange
            double high = 0.812, mid = 0.562, low = 0.312;

            // Assert
            Assert.That(_referenceData.EmissionsFactor.High, Is.EqualTo(high));
            Assert.That(_referenceData.EmissionsFactor.Medium, Is.EqualTo(mid));
            Assert.That(_referenceData.EmissionsFactor.Low, Is.EqualTo(low));
        }
    }
}