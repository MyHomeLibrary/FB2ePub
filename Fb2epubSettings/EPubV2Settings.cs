using System.Xml.Serialization;
using EPubLibraryContracts.Settings;
using Fb2epubSettings.AppleSettings.ePub_v2;

namespace Fb2epubSettings
{
    [XmlRoot(ElementName = "EPubV2Settings")]
    public class EPubV2Settings : IEPubV2Settings
    {
        private bool _enableAdobeTemplate;
        private string _adobeTemplatePath = string.Empty;
        private readonly IAppleConverterePub2Settings _appleEPubSettings = new AppleConverterePub2Settings();
        private bool _addCalibreMetadata = true;

        public void SetupDefaults()
        {
            _enableAdobeTemplate = false;
            _adobeTemplatePath = string.Empty;
            _addCalibreMetadata = true;
            _appleEPubSettings.SetupDefaults();
        }

        /// <summary>
        /// Get/Set if Calibre metadata should be added
        /// </summary>
        [XmlElement(ElementName = "AddCalibreMetadata")]
        public bool AddCalibreMetadata
        {
            get { return _addCalibreMetadata; }
            set { _addCalibreMetadata = value; }
        }

        /// <summary>
        /// Enable/Disable embedding of Adobe XPGT Template into a resulting file
        /// </summary>
        [XmlElement(ElementName = "EnableAdobeTemplateUsage")]
        public bool EnableAdobeTemplate
        {
            get { return _enableAdobeTemplate; }
            set { _enableAdobeTemplate = value; }
        }

        /// <summary>
        /// Return reference to set of apple/iBook related settings
        /// </summary>
        [XmlElement(ElementName = "AppleConverterEPub2Settings")]
        public IAppleConverterePub2Settings AppleConverterEPubSettings
        {
            get { return _appleEPubSettings; }
            set { _appleEPubSettings.CopyFrom(value); }
        }

        /// <summary>
        /// Path to location of Adobe XPGT template
        /// </summary>
        [XmlElement(ElementName = "AdobeTemplatePath")]
        public string AdobeTemplatePath
        {
            get { return _adobeTemplatePath; }
            set { _adobeTemplatePath = value; }
        }


        public void CopyFrom(IEPubV2Settings temp)
        {
            _enableAdobeTemplate = temp.EnableAdobeTemplate;
            _adobeTemplatePath = temp.AdobeTemplatePath;
            _addCalibreMetadata = temp.AddCalibreMetadata;
            _appleEPubSettings.CopyFrom(temp.AppleConverterEPubSettings);

        }
    }
}
