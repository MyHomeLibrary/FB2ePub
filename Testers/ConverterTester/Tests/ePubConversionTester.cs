using System;
using System.IO;
using ConverterContracts;
using ConverterContracts.Settings;
using FB2EPubConverter;
using System.ComponentModel;
using System.Text;
using Fb2epubSettings;

namespace ConverterTester.Tests
{
    class EPubConversionTester : ITester
    {
        private Exception _lastException;
        private readonly EPubVersion _version;


        public EPubConversionTester(EPubVersion version)
        {
            _version = version;
        }

        public bool Test()
        {
            try
            {
                ConverterSettingsFile settingsFile = new ConverterSettingsFile();
                string filePath;
                var settings = new ConverterSettings();
                DefaultSettingsLocatorHelper.EnsureDefaultSettingsFilePresent(out filePath, settings);
                settingsFile.Load(filePath);
                settings.StandardVersion = _version;
                settings.FB2ImportSettings.FixMode = FixOptions.UseFb2Fix;
                IFb2EPubConverterEngine converter = ConvertProcessor.CreateConverterEngine(settingsFile.Settings);
                var path =  new StringBuilder();
                path.AppendFormat(@"{0}\TestFiles\Test_001.fb2", Directory.GetCurrentDirectory());
                converter.LoadAndCheckFB2Files(path.ToString());
                string outPath = Path.GetTempPath();
                converter.Save(outPath);
            }
            catch (Exception ex)
            {
                _lastException = ex;
                return false;
            }
            return true;
        }

        public string Name
        {
            get { return "ePub conversion"; }
        }

        public Exception TestError
        {
            get { return _lastException; }
        }

    
    }
}
