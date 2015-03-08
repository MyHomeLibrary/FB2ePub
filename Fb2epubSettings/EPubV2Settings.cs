using System.Xml;
using System.Xml.Schema;
using EPubLibraryContracts.Settings;
using Fb2epubSettings.AppleSettings.ePub_v2;

namespace Fb2epubSettings
{
    public class EPubV2Settings : IEPubV2Settings 
    {
        private bool _enableAdobeTemplate;
        private string _adobeTemplatePath = string.Empty;
        private readonly AppleConverterePub2Settings _appleEPubSettings = new AppleConverterePub2Settings();
        private bool _addCalibreMetadata = true;

        #region constants

        public EPubV2Settings()
        {
            HTMLFileMaxSize = 245 * 1024;
        }

        public const string V2SettingsElementName = "EPubV2Settings";

        private const string AddCalibreMetadataElementName = "AddCalibreMetadata";
        private const string EnableAdobeTemplateUsageElementName = "EnableAdobeTemplateUsage";
        private const string AdobeTemplatePathElementName = "AdobeTemplatePath";
        private const string HTMLFileMaxSizeAllowedElementName = "HTMLFileMaxSizeAllowed";

        #endregion

        public void SetupDefaults()
        {
            _enableAdobeTemplate = false;
            _adobeTemplatePath = string.Empty;
            _addCalibreMetadata = true;
            _appleEPubSettings.SetupDefaults();
            HTMLFileMaxSize = 245 * 1024;
        }

        /// <summary>
        /// Get/Set if Calibre metadata should be added
        /// </summary>
        public bool AddCalibreMetadata
        {
            get { return _addCalibreMetadata; }
            set { _addCalibreMetadata = value; }
        }

        /// <summary>
        /// Enable/Disable embedding of Adobe XPGT Template into a resulting file
        /// </summary>
        public bool EnableAdobeTemplate
        {
            get { return _enableAdobeTemplate; }
            set { _enableAdobeTemplate = value; }
        }

        /// <summary>
        /// Return reference to set of apple/iBook related settings
        /// </summary>
        public IAppleConverterePub2Settings AppleConverterEPubSettings
        {
            get { return _appleEPubSettings; }
            set { _appleEPubSettings.CopyFrom(value); }
        }

        /// <summary>
        /// Path to location of Adobe XPGT template
        /// </summary>
        public string AdobeTemplatePath
        {
            get { return _adobeTemplatePath; }
            set { _adobeTemplatePath = value; }
        }

        public long HTMLFileMaxSize { get; set; }

        public void CopyFrom(IEPubV2Settings temp)
        {
            _enableAdobeTemplate = temp.EnableAdobeTemplate;
            _adobeTemplatePath = temp.AdobeTemplatePath;
            _addCalibreMetadata = temp.AddCalibreMetadata;
            _appleEPubSettings.CopyFrom(temp.AppleConverterEPubSettings);

        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case AddCalibreMetadataElementName:
                            _addCalibreMetadata = reader.ReadElementContentAsBoolean();
                            continue;
                        case EnableAdobeTemplateUsageElementName:
                            _enableAdobeTemplate = reader.ReadElementContentAsBoolean();
                            continue;
                        case AppleConverterePub2Settings.AppleConverterEPub2SettingsElementName:
                            _appleEPubSettings.ReadXml(reader.ReadSubtree());
                            break;
                        case AdobeTemplatePathElementName:
                            _adobeTemplatePath = reader.ReadElementContentAsString();
                            continue;
                        case HTMLFileMaxSizeAllowedElementName:
                            HTMLFileMaxSize = reader.ReadElementContentAsLong();
                            continue;
                    }
                }
                reader.Read();
            }            
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(V2SettingsElementName);

            writer.WriteStartElement(AddCalibreMetadataElementName);
            writer.WriteValue(_addCalibreMetadata);
            writer.WriteEndElement();

            writer.WriteStartElement(EnableAdobeTemplateUsageElementName);
            writer.WriteValue(_enableAdobeTemplate);
            writer.WriteEndElement();

            _appleEPubSettings.WriteXml(writer);

            writer.WriteStartElement(AdobeTemplatePathElementName);
            writer.WriteValue(_adobeTemplatePath);
            writer.WriteEndElement();

            writer.WriteStartElement(HTMLFileMaxSizeAllowedElementName);
            writer.WriteValue(HTMLFileMaxSize.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
