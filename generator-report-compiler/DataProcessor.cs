using System.IO;
using System.Threading.Tasks;
using Brady.Serialization;

namespace Brady
{
    public class DataProcessor
    {
        public static Task<GenerationReport> Process(string sourceFile)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(sourceFile)) return null;
                var processor = new DataProcessor(sourceFile);

                return processor._theReport;
            });
        }

        private readonly GenerationReport _theReport;

        private DataProcessor(string sourceFile)
        {
            var report = GenerationReport.Deserialize(sourceFile);
            _theReport = report;
        }

    }
}