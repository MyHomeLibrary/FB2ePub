using System.Xml.Serialization;

namespace EPubLibraryContracts.Settings
{
    public enum EPubV3SubStandard
    {
        V30,
        V301,
    }

    public interface IEPubV3Settings : IXmlSerializable
    {
        void SetupDefaults();
        void CopyFrom(IEPubV3Settings temp);

        EPubV3SubStandard V3SubStandard { get; set; }
        bool GenerateV2CompatibleTOC { get; set; }
        ulong HTMLFileMaxSize { get; set; }

    }
}
