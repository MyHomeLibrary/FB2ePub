using System;
using System.IO;
using Fb2epubSettings;

namespace ConverterTester.Tests
{
    class ConfigFileTester : ITester
    {
        private Exception _lastException;

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
                _lastException = ex;
                return false;
            }
            return true;
        }

        public string Name
        {
            get { return "Configuration File tester"; }
        }

        public Exception TestError
        {
            get { return _lastException; }
        }
    }
}
