using FontsSettings;
using System;
using System.Xml.Serialization;

namespace Fb2epubSettings
{
    /// <summary>
    /// Fix mode options
    /// </summary>
    public enum FixOptions
    {
        DoNotFix, // do not attempt to fix
        MinimalFix, // try minimal (internal) fix
        UseFb2Fix, // use Fb2Fix if prev. failed
        Fb2FixAlways, // always use Fb2Fix
    }

    public enum IgnoreTitleOptions
    {
        IgnoreNothing = 0,
        IgnoreMainTitle,
        IgnoreSourceTitle,
        IgnorePublishTitle,
        IgnoreMainAndSource,
        IgnoreMainAndPublish,
        IgnoreSourceAndPublish,
    }

    public enum EPubVersion
    {
        VEpub20 =   2,
        VePub30 =   3,
    }

    [XmlRoot(ElementName = "ConverterSettings")]
    public class ConverterSettings
    {
        #region private_members
        private string _outputPath = string.Empty;
        private string _resourcesPath = string.Empty;
        private EPubVersion _standardVersion = EPubVersion.VEpub20;
        private readonly FB2ImportSettings _fb2Settings = new FB2ImportSettings();
        private readonly EPubV2Settings _v2Settings = new EPubV2Settings();
        private readonly EPubV3Settings _v3Settings = new EPubV3Settings();
        private  readonly EPubCommonSettings _commonSettings = new EPubCommonSettings();
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
        public FB2ImportSettings Fb2ImportSettings
        {
            get { return _fb2Settings; }
            set { _fb2Settings.CopyFrom(value); }
        }


        /// <summary>
        /// Get/Set common settings
        /// </summary>
        [XmlElement(ElementName = "CommonSettings")]
        public EPubCommonSettings CommonSettings
        {
            get { return _commonSettings; }
            set { _commonSettings.CopyFrom(value); }
        }


        /// <summary>
        /// Get/Set V2 settings
        /// </summary>
        [XmlElement(ElementName = "V2Settings")]
        public EPubV2Settings V2Settings
        {
            get { return _v2Settings; }
            set { _v2Settings.CopyFrom(value); }
        }

        /// <summary>
        /// Get/Set V3 settings
        /// </summary>
        [XmlElement(ElementName = "V3Settings")]
        public EPubV3Settings V3Settings
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
