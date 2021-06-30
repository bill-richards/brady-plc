using System.IO;
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

        public static GenerationReport DeserializeFromFile(string fullPath)
        {
            var serializer = new XmlSerializer(typeof(GenerationReport));
            using Stream reader = new FileStream(fullPath, FileMode.Open);
            return (GenerationReport)serializer.Deserialize(reader);
        }
    }
}