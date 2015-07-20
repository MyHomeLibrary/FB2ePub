using System;
using System.IO;
using ConverterContracts;
using ConverterContracts.Settings;
using FB2EPubConverter;
using System.Text;
using EPubLibraryContracts.Settings;
using Fb2epubSettings;

namespace ConverterTester.Tests
{
    class EPubConversionTester 
    {
        private readonly EPubVersion _version;


        public EPubConversionTester(EPubVersion version)
        {
            _version = version;
        }

        public void Test()
        {
                ConverterSettingsFile settingsFile = new ConverterSettingsFile();
                string filePath;
                var settings = new ConverterSettings();
                DefaultSettingsLocatorHelper.EnsureDefaultSettingsFilePresent(out filePath, settings);
                settingsFile.Load(filePath);
                settingsFile.Settings.StandardVersion = _version;
                settingsFile.Settings.FB2ImportSettings.FixMode = FixOptions.UseFb2Fix;
                settingsFile.Settings.V3Settings.FootnotesCreationMode = FootnotesGenerationMode.Combined;
                IFb2EPubConverterEngine converter = ConvertProcessor.CreateConverterEngine(settingsFile.Settings);
                var path =  new StringBuilder();
                path.AppendFormat(@"{0}\TestFiles\Test_001.fb2", Directory.GetCurrentDirectory());
                converter.LoadAndCheckFB2Files(path.ToString());
                string outPath = Path.GetTempPath();
                converter.Save(outPath);
        }

    
    }
}
