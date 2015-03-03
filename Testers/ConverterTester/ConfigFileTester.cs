using System;
using System.IO;
using Fb2epubSettings;

namespace ConverterTester
{
    class ConfigFileTester : ITester
    {
        public bool Test()
        {
            try
            {
                var converterFile = new ConverterSettingsFile();
                string fileName = Path.GetTempFileName();
                converterFile.Settings.SetupDefaults();
                converterFile.Save(fileName);
                converterFile.Load(fileName);
                File.Delete(fileName);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public string Name
        {
            get { return "Configuration File tester"; }
        }
    }
}
