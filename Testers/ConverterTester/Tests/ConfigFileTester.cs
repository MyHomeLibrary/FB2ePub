using System.IO;
using Fb2epubSettings;

namespace ConverterTester.Tests
{
    class ConfigFileTester
    {
        public void Test()
        {
            var converterFile = new ConverterSettingsFile();
            string fileName = Path.GetTempFileName();
            converterFile.Settings.SetupDefaults();
            converterFile.Save(fileName);
            converterFile.Load(fileName);
            File.Delete(fileName);
        }


    }
}
