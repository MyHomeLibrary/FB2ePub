using System.Xml.Serialization;

namespace EPubLibraryContracts.Settings
{
    /// <summary>
    /// Defines types ("platforms") of the apple devices it distinguish between
    /// in order to apply different settings
    /// </summary>
    public enum AppleTargetPlatform
    {
        [XmlEnum(Name = "")]
        NotSet = 0,
        [XmlEnum(Name = "iPad")]
        iPad = 1,
        [XmlEnum(Name = "iPhone")]
        iPhone = 2,
        [XmlEnum(Name = "Any")]
        All = 3,
    };


    /// <summary>
    /// Defines orientation lock
    /// preventing the device to display book in specific orientation
    /// </summary>
    public enum AppleOrientationLock
    {
        [XmlEnum(Name = "Disabled")]
        None = 0,
        [XmlEnum(Name = "LandscapeOnly")]
        LandscapeOnly = 1,
        [XmlEnum(Name = "PortraitOnly")]
        PortraitOnly = 2,

        LastValue = PortraitOnly,
    };


    public interface IAppleEPub2PlatformSettings
    {
        bool UseCustomFonts { get; set; }
        bool OpenToSpread { get; set; }
        AppleTargetPlatform Name { get; set; }
        bool FixedLayout { get; set; }
        AppleOrientationLock OrientationLock { get; set; }


        void CopyFrom(IAppleEPub2PlatformSettings otherSettings);
    }
}
