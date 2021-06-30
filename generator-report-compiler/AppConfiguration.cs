using System;
using Microsoft.Extensions.Configuration;

namespace Brady
{
    public class AppConfiguration
    {
        public string InputDirectory { get; set; }
        public string OutputDirectory { get; set; }
        public string ReferenceDataFilePath { get; set; }

        private static AppConfiguration _theConfiguration;

        public static AppConfiguration GetConfiguration()
        {
            if (_theConfiguration != null) return _theConfiguration;

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection(nameof(AppConfiguration));
            _theConfiguration = section.Get<AppConfiguration>();

            return _theConfiguration;
        } 
    }
}