using System.Xml.Serialization;

namespace EPubLibraryContracts.Settings
{
    public interface IEPubCommonSettings : IXmlSerializable
    {
        void CopyFrom(IEPubCommonSettings temp);
        void SetupDefaults();

        bool TransliterateToc { get; set; }
        bool FlatStructure { get; set; }
    }
}
