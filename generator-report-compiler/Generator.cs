using System;
using System.Xml.Serialization;

namespace Brady
{
    public abstract class Generator
    {
        public string Name { get; set; }

        [XmlArray("Generation")]
        [XmlArrayItem("Day")]
        public ReportCollection<Day> Days { get; } = new ReportCollection<Day>();

        //public void AddGenerationData(DateTime date, double energy, double price) 
        //    => Days.Add(new Day { Date = date, Energy = energy, Price = price });
    }
}