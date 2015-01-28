using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ConverterContracts.Settings;

namespace Fb2epubSettings.AppleSettings.ePub_v2
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AppleConverterePub2Settings : IAppleConverterePub2Settings
    {
        private readonly List<IAppleEPub2PlatformSettings> _platforms = new List<IAppleEPub2PlatformSettings>();


        [XmlArray("Platforms"), XmlArrayItem(typeof(IAppleEPub2PlatformSettings), ElementName = "Platform")]
        public List<IAppleEPub2PlatformSettings> Platforms { get { return _platforms; } }


        public void SetupDefaults()
        {
            _platforms.Clear();
            var platform = new AppleEPub2PlatformSettings { Name = AppleTargetPlatform.All, 
                                                                UseCustomFonts = true, 
                                                                OpenToSpread = false, 
                                                                OrientationLock = AppleOrientationLock.None,
                                                                FixedLayout = false};
            _platforms.Add(platform);
        }


        public void CopyFrom(IAppleConverterePub2Settings appleConverterSettings)
        {
            if (appleConverterSettings == null)
            {
                throw new ArgumentNullException("appleConverterSettings");
            }
            if (appleConverterSettings == this)
            {
                return;
            }
            _platforms.Clear();
            foreach (var platform in appleConverterSettings.Platforms)
            {
                AppleEPub2PlatformSettings platformTo = new AppleEPub2PlatformSettings();
                platformTo.CopyFrom(platform);
                _platforms.Add(platformTo);
            }
        }
    }
}
