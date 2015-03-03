using System.Collections.Generic;
using System.Xml.Serialization;

namespace EPubLibraryContracts.Settings
{
    public interface IAppleConverterePub2Settings : IXmlSerializable
    {
        void SetupDefaults();
        void CopyFrom(IAppleConverterePub2Settings appleConverterSettings);

        List<IAppleEPub2PlatformSettings> Platforms { get; }
    }
}
