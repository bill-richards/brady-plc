using System.Linq;
using System.Xml.Serialization;

namespace Brady.Serialization
{
    public abstract class Generator
    {
        public string Name { get; set; }

        [XmlArray("Generation")]
        [XmlArrayItem("Day")]
        public ReportCollection<Day> Days { get; } = new ReportCollection<Day>();

        [XmlIgnore]
        public double TotalGenerationValue
            => double.Parse(Days.AsEnumerable().Sum(day => day.GenerationValue).ToString("0.#########"));
    }
}