using System.IO;
using System.Reflection;

namespace Brady
{
    public static class TestHelper
    {
        public static void RemoveTestOutputFileAndDirectory(string file, string directory = "")
        {
            if (!file.IsNullOrWhitespace())
            {
                try { File.Delete(file); }
                catch { /* ignored */ }
            }

            if (directory.IsNullOrWhitespace()) return;
            try { Directory.Delete(directory); }
            catch { /* ignored */ }
        }

        private static string _location;

        public static string GetCurrentDirectory()
        {
            if (_location != null) return _location;

            var path = Assembly.GetExecutingAssembly().Location;
            _location = Path.GetDirectoryName(path);

            return GetCurrentDirectory();
        }

    }
}