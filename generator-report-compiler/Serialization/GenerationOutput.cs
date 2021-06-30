using System.IO;
using System.Xml.Serialization;

namespace Brady.Serialization
{
    public class GenerationOutput
    {
        [XmlArray("Totals")]
        [XmlArrayItem("Generator")]
        public ReportCollection<GeneratorOutput> GeneratorOutputs { get; } = new ReportCollection<GeneratorOutput>();

        [XmlArray("MaxEmissionGenerators")]
        [XmlArrayItem("Day")]
        public ReportCollection<DayOutput> DayOutputs { get; } = new ReportCollection<DayOutput>();

        [XmlArray("ActualHeatRates")]
        [XmlArrayItem("ActualHeatRate")]
        public ReportCollection<ActualHeatRate> HeatRates { get; } = new ReportCollection<ActualHeatRate>();

        public TCollectionType AddOutput<TCollectionType>(TCollectionType item) 
        {
            if (item.GetType() == typeof(DayOutput)) { DayOutputs.Add(item as DayOutput); }
            else if (item.GetType() == typeof(GeneratorOutput)) { GeneratorOutputs.Add(item as GeneratorOutput); }
            else if (item.GetType() == typeof(ActualHeatRate)) { HeatRates.Add(item as ActualHeatRate); }
            return item;
        }

        public void SerializeToFile(string fullPath)
        {
            var x = new XmlSerializer(typeof(GenerationOutput));
            using TextWriter writer = new StreamWriter(fullPath);
            x.Serialize(writer, this);
            x = null;
        }
    }
}