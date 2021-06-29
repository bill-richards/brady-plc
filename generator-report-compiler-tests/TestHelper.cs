using System.IO;

namespace Brady
{
    public static class TestHelper
    {
        public static void RemoveTestOutputFileAndDirectory(string file, string directory)
        {
            try { File.Delete(file); }
            catch { /* ignored */ }
            try { Directory.Delete(directory); }
            catch { /* ignored */ }
        }
    }
}