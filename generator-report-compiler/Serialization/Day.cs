using System;
using System.Xml.Serialization;

namespace Brady.Serialization
{
    public class Day
    {
        public DateTime Date { get; set;  }
        public double Energy { get; set; }
        public double Price { get; set; }

        [XmlIgnore]
        public double GenerationValue { get; private set; }
        [XmlIgnore]
        public double Emissions { get; private set; }

        [XmlIgnore]
        public string Name { get; set; }

        public double CalculateGenerationValue(double valueFactor)
        {
            GenerationValue = (Energy * Price * valueFactor);
            return GenerationValue;
        }

        public void CalculateEmissions(double emissionRating, double emissionFactor)
            => Emissions = Energy * emissionRating * emissionFactor;
    }


}
