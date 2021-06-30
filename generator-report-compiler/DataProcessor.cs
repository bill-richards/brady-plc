using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using Brady.Serialization;

namespace Brady
{
    public class DataProcessor
    {
        public static Task<GenerationOutput> Process(string sourceFile, string referenceDataFile)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(sourceFile)) return null;
                var processor = new DataProcessor(sourceFile, referenceDataFile);
                return processor.Process();
            });
        }

        private readonly GenerationOutput _output;
        private readonly GenerationReport _theReport;
        private readonly ReferenceData _referenceData;

        private DataProcessor(string sourceFile, string referenceDataFile)
        {
            _output = new GenerationOutput();
            _theReport = GenerationReport.DeserializeFromFile(sourceFile);
            _referenceData = ReferenceData.DeserializeFromFile(referenceDataFile);
        }

        private GenerationOutput Process()
        {
            CalculateGenerationValues();
            ConstructGenerationOutput();
            return _output;
        }

        private void CalculateGenerationValues()
        {
            _theReport.CalculateOffshoreWindGenerationValues(_referenceData.ValueFactor.Low);
            _theReport.CalculateOnshoreWindGenerationValues(_referenceData.ValueFactor.High);
            _theReport.CalculateGasGenerationAndEmissionsValues(_referenceData.ValueFactor.Medium, _referenceData.EmissionsFactor.Medium);
            _theReport.CalculateCoalGenerationAndEmissionsValues(_referenceData.ValueFactor.Medium, _referenceData.EmissionsFactor.High);
        }

        private void ConstructGenerationOutput()
        {
            var generators = new List<Generator>();
            generators.AddRange(_theReport.Wind.AsEnumerable());
            generators.AddRange(_theReport.Gas.AsEnumerable());
            generators.AddRange(_theReport.Coal.AsEnumerable());
            
            foreach (var generator in generators)
            {
                var generatorName = generator.Name;
                var generationValue = generator.TotalGenerationValue;
                AddGeneratorOutput(generatorName, generationValue);
            }
            AddEmissionsToOutput(generators);
            AddHeatRatesToOutput(_theReport.Coal.AsEnumerable());
        }

        private Task AddHeatRatesToOutput(IEnumerable<CoalGenerator> generators)
        {
            return Task.Run(() =>
            {
                foreach (var generator in generators)
                {
                    _output.AddOutput(new ActualHeatRate
                    {
                        HeatRate = double.Parse((generator.TotalHeatInput / generator.ActualNetGeneration).ToString("0.#########")),
                        Name = generator.Name
                    });
                }
            });
        }

        private Task AddEmissionsToOutput(IEnumerable<Generator> generators)
        {
            return Task.Run(() =>
            {
                var days = new List<Day>();
                foreach (var generator in generators)
                {
                    days.AddRange(generator.Days.AsEnumerable());
                }

                foreach (var day in days.GroupBy(t => t.Date))
                {
                    var highestEmissions = day.OrderByDescending(d => d.Emissions).First();
                    _output.AddOutput(new EmissionsOutput
                    {
                        Name = highestEmissions.Name,
                        Date = highestEmissions.Date,
                        Emission = highestEmissions.Emissions
                    });
                }
            });
        }

        private void AddGeneratorOutput(string generatorName, double total) 
            => _output.AddOutput(new GeneratorOutput { Name = generatorName, Total = total });
    }
}