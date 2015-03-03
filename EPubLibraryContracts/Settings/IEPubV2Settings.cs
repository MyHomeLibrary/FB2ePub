using System.Xml.Serialization;

namespace EPubLibraryContracts.Settings
{
    public interface IEPubV2Settings : IXmlSerializable
    {
        void SetupDefaults();
        bool AddCalibreMetadata { get; set; }
        bool EnableAdobeTemplate { get; set; }
        IAppleConverterePub2Settings AppleConverterEPubSettings { get; set; }
        string AdobeTemplatePath { get; set; }

        void CopyFrom(IEPubV2Settings temp);
    }
}
