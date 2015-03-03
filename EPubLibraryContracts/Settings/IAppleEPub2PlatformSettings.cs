using System.Xml.Serialization;

namespace EPubLibraryContracts.Settings
{
    /// <summary>
    /// Defines types ("platforms") of the apple devices it distinguish between
    /// in order to apply different settings
    /// </summary>
    public enum AppleTargetPlatform
    {
        NotSet = 0,
        iPad = 1,
        iPhone = 2,
        All = 3,
    };


    /// <summary>
    /// Defines orientation lock
    /// preventing the device to display book in specific orientation
    /// </summary>
    public enum AppleOrientationLock
    {
        None = 0,
        LandscapeOnly = 1,
        PortraitOnly = 2,

        LastValue = PortraitOnly,
    };


    public interface IAppleEPub2PlatformSettings : IXmlSerializable
    {
        bool UseCustomFonts { get; set; }
        bool OpenToSpread { get; set; }
        AppleTargetPlatform Name { get; set; }
        bool FixedLayout { get; set; }
        AppleOrientationLock OrientationLock { get; set; }


        void CopyFrom(IAppleEPub2PlatformSettings otherSettings);
    }
}
