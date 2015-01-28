using System;
using System.Xml.Serialization;
using ConverterContracts.Settings;
using EPubLibraryContracts.Settings;

namespace Fb2epubSettings
{


    public enum EPubVersion
    {
        VEpub20 =   2,
        VePub30 =   3,
    }

    [XmlRoot(ElementName = "ConverterSettings")]
    public class ConverterSettings : IConverterSettings
    {
        #region private_members
        private string _outputPath = string.Empty;
        private string _resourcesPath = string.Empty;
        private EPubVersion _standardVersion = EPubVersion.VEpub20;
        private readonly IFB2ImportSettings _fb2Settings = new FB2ImportSettings();
        private readonly IEPubV2Settings _v2Settings = new EPubV2Settings();
        private readonly IEPubV3Settings _v3Settings = new EPubV3Settings();
        private  readonly IEPubCommonSettings _commonSettings = new EPubCommonSettings();
        #endregion

        public ConverterSettings()
        {
            SetupDefaults();
        }

        /// <summary>
        /// Creates default settings set
        /// </summary>
        public void SetupDefaults()
        {
            _resourcesPath = string.Empty;
            _fb2Settings.SetupDefaults();
            _commonSettings.SetupDefaults();
            _v2Settings.SetupDefaults();
            _v3Settings.SetupDefaults();


            //_standardVersion = EPubVersion.VePub30;

        }

        #region serializable_public_Properties




        /// <summary>
        /// Get/Set common settings
        /// </summary>
        [XmlElement(ElementName = "Fb2ImportSettings")]
        public IFB2ImportSettings Fb2ImportSettings
        {
            get { return _fb2Settings; }
            set { _fb2Settings.CopyFrom(value); }
        }


        /// <summary>
        /// Get/Set common settings
        /// </summary>
        [XmlElement(ElementName = "CommonSettings")]
        public IEPubCommonSettings CommonSettings
        {
            get { return _commonSettings; }
            set { _commonSettings.CopyFrom(value); }
        }


        /// <summary>
        /// Get/Set V2 settings
        /// </summary>
        [XmlElement(ElementName = "V2Settings")]
        public IEPubV2Settings V2Settings
        {
            get { return _v2Settings; }
            set { _v2Settings.CopyFrom(value); }
        }

        /// <summary>
        /// Get/Set V3 settings
        /// </summary>
        [XmlElement(ElementName = "V3Settings")]
        public IEPubV3Settings V3Settings
        {
            get { return _v3Settings; }
            set { _v3Settings.CopyFrom(value); }
        }

        /// <summary>
        /// Get/Set path to conversion resources location (CSS, fonts etc)
        /// </summary>
        [XmlElement(ElementName = "ResourcesPath")]
        public string ResourcesPath 
        {
            get { return _resourcesPath; }
            set { _resourcesPath = value; }
        }


        /// <summary>
        /// Version of ePub to generate
        /// </summary>
        [XmlElement(ElementName = "EPUBVersion")]
        public EPubVersion StandardVersion
        {
            get { return _standardVersion; }
            set { _standardVersion = value; }
        }



        #endregion

        #region nonserializable_public_Properties

        /// <summary>
        /// Get/Set output path used in case output file name does not includes path
        /// </summary>
        [XmlIgnore]
        public string OutPutPath
        {
            get { return _outputPath; }
            set 
            { 
                _outputPath = value;
                if (!_outputPath.EndsWith("\\") && !_outputPath.EndsWith("/") && !string.IsNullOrEmpty(_outputPath))
                {
                    _outputPath += "\\";
                }

            }
        }
        #endregion



        public void CopyFrom(ConverterSettings temp)
        {
            if (temp == null)
            {
                throw new ArgumentNullException("temp");
            }
            if (temp == this)
            {
                return;
            }

            _outputPath = temp._outputPath;
            _resourcesPath = temp._resourcesPath;
            _standardVersion = temp._standardVersion;
            _commonSettings.CopyFrom(temp.CommonSettings);
            _v2Settings.CopyFrom(temp.V2Settings);
            _v3Settings.CopyFrom(temp.V3Settings);
        }

    }
}
