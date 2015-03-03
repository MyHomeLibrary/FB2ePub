using System.Xml.Serialization;
using EPubLibraryContracts.Settings;

namespace ConverterContracts.Settings
{
    public enum EPubVersion
    {
        V2 = 2,
        V3 = 3,
    }

    public interface IConverterSettings : IXmlSerializable
    {
        void SetupDefaults();
        IFB2ImportSettings FB2ImportSettings { get; set; }
        IEPubCommonSettings CommonSettings { get; set; }
        IEPubV2Settings V2Settings { get; set; }
        IEPubV3Settings V3Settings { get; set; }
        string ResourcesPath { get; set; }
        EPubVersion StandardVersion { get; set; }
        string OutPutPath { get; set; }
        void CopyFrom(IConverterSettings temp);
    }
}
