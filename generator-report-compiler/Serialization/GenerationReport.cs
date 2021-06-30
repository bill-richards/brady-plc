using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Brady.Serialization
{
    public class GenerationReport
    {
        public ReportCollection<WindGenerator> Wind { get; } = new ReportCollection<WindGenerator>();
        public ReportCollection<GasGenerator> Gas { get; } = new ReportCollection<GasGenerator>();
        public ReportCollection<CoalGenerator> Coal { get; } = new ReportCollection<CoalGenerator>();

        public TGeneratorType Add<TGeneratorType>(TGeneratorType generator) where TGeneratorType : Generator
        {
            if (generator.GetType() == typeof(WindGenerator)) { Wind.Add(generator as WindGenerator);}
            else if (generator.GetType() == typeof(GasGenerator)) { Gas.Add(generator as GasGenerator);}
            else { Coal.Add(generator as CoalGenerator);}

            return generator;
        }

        public void CalculateOffshoreWindGenerationValues(double valueFactor) 
            => CalculateWindGenerationValue(valueFactor, "Offshore");

        public void CalculateOnshoreWindGenerationValues(double valueFactor) 
            => CalculateWindGenerationValue(valueFactor, "Onshore");

        public void CalculateCoalGenerationAndEmissionsValues(double valueFactor, double emissionsFactor)
        {
            foreach (var generator in Coal.AsEnumerable())
            {
                CalculateGenerationValue(generator, valueFactor, emissionsFactor, true);
            }
        }

        public void CalculateGasGenerationAndEmissionsValues(double valueFactor, double emissionsFactor)
        {
            foreach (var generator in Gas.AsEnumerable())
            {
                CalculateGenerationValue(generator, valueFactor, emissionsFactor, true);
            }
        }

        private void CalculateWindGenerationValue(double valueFactor, string location)
        {
            foreach (var generator in Wind.AsEnumerable().Where(g  => g.Location == location))
            {
                CalculateGenerationValue(generator, valueFactor);
            }
        }

        private void CalculateGenerationValue(Generator generator, double valueFactor, double emissionFactor = 0, bool calculateEmissions = false)
        {
            foreach (var day in generator.Days.AsEnumerable())
            {
                day.Name = generator.Name;
                day.CalculateGenerationValue(valueFactor);

                if(generator is EmissionsRatedGenerator ratedGenerator)
                    day.CalculateEmissions(ratedGenerator.EmissionsRating, emissionFactor);
            }
        }

        private void CalculateEmissions(EmissionsRatedGenerator generator, double emissionFactor)
        {
            foreach (var day in generator.Days.AsEnumerable())
            {
                day.CalculateEmissions(generator.EmissionsRating, emissionFactor);
            }
        }

        public static GenerationReport DeserializeFromFile(string fullPath)
        {
            var serializer = new XmlSerializer(typeof(GenerationReport));
            using Stream reader = new FileStream(fullPath, FileMode.Open);
            return (GenerationReport)serializer.Deserialize(reader);
        }
    }
}