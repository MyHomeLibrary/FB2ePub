using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConverterContracts;
using ConverterContracts.ComInterfaces;
using ConverterContracts.Settings;
using Fb2epubSettings;

namespace ConverterTester
{
    class TestConvertProcessorSettings : IConvertProcessorSettings
    {
        private readonly IConverterSettings _settings = new ConverterSettings();

        public IConverterSettings Settings
        {
            get { throw new NotImplementedException(); }
        }

        public bool DeleteSource
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool LookInSubFolders
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public PathSearchOptions SearchMask
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IProgressUpdateInterface ProgressCallbacks
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string SettingsFileToUse
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool SingleFile
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
