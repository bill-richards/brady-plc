using System;
using Brady.Serialization;

namespace Brady
{
    public static class TestDataForOutput
    {
        public static GenerationOutput Output { get; private set; }

        public static void CreateGenerationOutput()
        {
            Output = new GenerationOutput();
            
            Output.AddOutput(new GeneratorOutput { Name = "Wind[Offshore]", Total = 1662.617445705 });
            Output.AddOutput(new GeneratorOutput { Name = "Wind[Onshore]", Total = 4869.453917394 });
            Output.AddOutput(new GeneratorOutput { Name = "Gas[1]", Total = 8512.254605520 });
            Output.AddOutput(new GeneratorOutput { Name = "Coal[1]", Total = 5341.716526632 });
            
            Output.AddOutput(new DayOutput { Name = "Coal[1]", Emission = 137.175004008, Date = DateTime.Parse("2017-01-01T00:00:00+00:00") });
            Output.AddOutput(new DayOutput { Name = "Coal[1]", Emission = 136.440767624, Date = DateTime.Parse("2017-01-02T00:00:00+00:00") });
            Output.AddOutput(new DayOutput { Name = "Gas[1]", Emission = 5.132380700, Date = DateTime.Parse("2017-01-03T00:00:00+00:00") });
            
            Output.AddOutput(new ActualHeatRate { Name = "Coal[1]", HeatRate = 1 });
        }
    }
}