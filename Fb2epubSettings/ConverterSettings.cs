using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ConverterContracts.Settings;
using EPubLibraryContracts.Settings;

namespace Fb2epubSettings
{

    public class ConverterSettings : IConverterSettings 
    {
        #region private_members
        private string _outputPath = string.Empty;
        private string _resourcesPath = string.Empty;
        private EPubVersion _standardVersion = EPubVersion.V2;
        private readonly FB2ImportSettings _fb2ImportSettings = new FB2ImportSettings();
        private readonly EPubV2Settings _v2Settings = new EPubV2Settings();
        private readonly EPubV3Settings _v3Settings = new EPubV3Settings();
        private readonly EPubCommonSettings _commonSettings = new EPubCommonSettings();
        #endregion


        #region constants

        public const string ConverterSettingsElementName = "ConverterSettings";

        private const string ResourcesPathElementName = "ResourcesPath";
        private const string EPUBVersionElementName = "EPUBVersion";

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
            _fb2ImportSettings.SetupDefaults();
            _commonSettings.SetupDefaults();
            _v2Settings.SetupDefaults();
            _v3Settings.SetupDefaults();
            _standardVersion = EPubVersion.V3;

        }

        #region serializable_public_Properties




        /// <summary>
        /// Get/Set common settings
        /// </summary>
        public IFB2ImportSettings FB2ImportSettings
        {
            get { return _fb2ImportSettings; }
            set { _fb2ImportSettings.CopyFrom(value); }
        }


        /// <summary>
        /// Get/Set common settings
        /// </summary>
        public IEPubCommonSettings CommonSettings
        {
            get { return _commonSettings; }
            set { _commonSettings.CopyFrom(value); }
        }


        /// <summary>
        /// Get/Set V2 settings
        /// </summary>
        public IEPubV2Settings V2Settings
        {
            get { return _v2Settings; }
            set { _v2Settings.CopyFrom(value); }
        }

        /// <summary>
        /// Get/Set V3 settings
        /// </summary>
        public IEPubV3Settings V3Settings
        {
            get { return _v3Settings; }
            set { _v3Settings.CopyFrom(value); }
        }

        /// <summary>
        /// Get/Set path to conversion resources location (CSS, fonts etc)
        /// </summary>
        public string ResourcesPath 
        {
            get { return _resourcesPath; }
            set { _resourcesPath = value; }
        }


        /// <summary>
        /// Version of ePub to generate
        /// </summary>
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



        public void CopyFrom(IConverterSettings temp)
        {
            if (temp == null)
            {
                throw new ArgumentNullException("temp");
            }
            if (temp == this)
            {
                return;
            }

            _outputPath = temp.OutPutPath;
            _resourcesPath = temp.ResourcesPath;
            _standardVersion = temp.StandardVersion;
            _commonSettings.CopyFrom(temp.CommonSettings);
            _v2Settings.CopyFrom(temp.V2Settings);
            _v3Settings.CopyFrom(temp.V3Settings);
        }


        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (!reader.EOF )
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case Fb2epubSettings.FB2ImportSettings.FB2ImportSettingsElementName:
                            _fb2ImportSettings.ReadXml(reader.ReadSubtree());
                            break;
                        case EPubCommonSettings.CommonSettingsElementName:
                            _commonSettings.ReadXml(reader.ReadSubtree());
                            break;
                        case EPubV2Settings.V2SettingsElementName:
                            _v2Settings.ReadXml(reader.ReadSubtree());
                            break;
                        case EPubV3Settings.EPubV3SettingsElementName:
                            _v3Settings.ReadXml(reader.ReadSubtree());
                            break;
                        case ResourcesPathElementName:
                            _resourcesPath = reader.ReadContentAsString();
                            continue;
                        case EPUBVersionElementName:
                            EPubVersion version;
                            string elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out version))
                            {
                                throw new InvalidDataException(string.Format("Invalid epub version : {0}", elementContent));
                            }
                            _standardVersion = version;
                            continue;
                    }                   
                }
                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(ConverterSettingsElementName);

            _fb2ImportSettings.WriteXml(writer);
            _commonSettings.WriteXml(writer);
            _v2Settings.WriteXml(writer);
            _v3Settings.WriteXml(writer);

            writer.WriteStartElement(ResourcesPathElementName);
            writer.WriteValue(_resourcesPath);
            writer.WriteEndElement();

            writer.WriteStartElement(EPUBVersionElementName);
            writer.WriteValue(_standardVersion.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
