using System.Collections.Generic;

namespace EPubLibraryContracts.Settings
{
    public interface IAppleConverterePub2Settings
    {
        void SetupDefaults();
        void CopyFrom(IAppleConverterePub2Settings appleConverterSettings);

        List<IAppleEPub2PlatformSettings> Platforms { get; }
    }
}
