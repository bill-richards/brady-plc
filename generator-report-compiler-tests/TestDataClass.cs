//using System;

//namespace Brady
//{
//    public class TestDataClass
//    {
//        public GenerationReport Report { get; private set; }

//        public void CreateGeneratorReport()
//        {
//            Report = new GenerationReport();
//            var wind = Report.Add(new WindGenerator { Name = "Wind[Offshore]", Location = "Offshore" });
//            wind.AddGenerationData(DateTime.Parse("2017-01-01T00:00:00+00:00"), 100.368, 20.148);
//            wind.AddGenerationData(DateTime.Parse("2017-01-02T00:00:00+00:00"), 90.843, 25.516);
//            wind.AddGenerationData(DateTime.Parse("2017-01-03T00:00:00+00:00"), 87.843, 22.015);

//            wind = Report.Add(new WindGenerator { Name = "Wind[Onshore]", Location = "Onshore" });
//            wind.AddGenerationData(DateTime.Parse("2017-01-01T00:00:00+00:00"), 56.578, 29.542);
//            wind.AddGenerationData(DateTime.Parse("2017-01-02T00:00:00+00:00"), 48.540, 22.954);
//            wind.AddGenerationData(DateTime.Parse("2017-01-03T00:00:00+00:00"), 98.167, 24.059);

//            var gas = Report.Add(new GasGenerator { Name = "Gas[1]", EmissionsRating = 0.038 });
//            gas.AddGenerationData(DateTime.Parse("2017-01-01T00:00:00+00:00"), 259.235, 15.837);
//            gas.AddGenerationData(DateTime.Parse("2017-01-02T00:00:00+00:00"), 235.975, 16.556);
//            gas.AddGenerationData(DateTime.Parse("2017-01-03T00:00:00+00:00"), 240.325, 17.551);

//            var coal = Report.Add(new CoalGenerator { Name = "Coal[1]", EmissionsRating = 0.482, TotalHeatInput = 11.815, ActualNetGeneration = 11.815 });
//            coal.AddGenerationData(DateTime.Parse("2017-01-01T00:00:00+00:00"), 350.487, 10.146);
//            coal.AddGenerationData(DateTime.Parse("2017-01-02T00:00:00+00:00"), 348.611, 11.815);
//            coal.AddGenerationData(DateTime.Parse("2017-01-03T00:00:00+00:00"), 0, 11.815);
//        }
//    }
//}