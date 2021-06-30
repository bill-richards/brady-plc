using System.Xml;

namespace Brady.Serialization
{
    public class ReferenceData
    {
        public class Factor
        {
            internal Factor() { }
            public double High { get; set; }
            public double Medium { get; set; }
            public double Low { get; set; }
        }

        private ReferenceData(XmlDocument document)
        {
            var root = document.DocumentElement;
            ValueFactor.Low = double.Parse(root.SelectSingleNode("./Factors/ValueFactor/Low").InnerText);
            ValueFactor.Medium = double.Parse(root.SelectSingleNode("./Factors/ValueFactor/Medium").InnerText);
            ValueFactor.High = double.Parse(root.SelectSingleNode("./Factors/ValueFactor/High").InnerText);

            EmissionsFactor.Low = double.Parse(root.SelectSingleNode("./Factors/EmissionsFactor/Low").InnerText);
            EmissionsFactor.Medium = double.Parse(root.SelectSingleNode("./Factors/EmissionsFactor/Medium").InnerText);
            EmissionsFactor.High = double.Parse(root.SelectSingleNode("./Factors/EmissionsFactor/High").InnerText);
        }

        public Factor ValueFactor { get; } = new Factor();
        public Factor EmissionsFactor { get; } = new Factor();

        public static ReferenceData DeserializeFromFile(string fullPath)
        {
            var document = new XmlDocument();
            document.Load(fullPath);
            return new ReferenceData(document);
        }
    }
}