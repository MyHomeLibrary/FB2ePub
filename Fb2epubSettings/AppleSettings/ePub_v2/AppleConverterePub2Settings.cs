using Fb2epubSettings.AppleSettings.ePub_v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Fb2epubSettings.AppleSettings
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AppleConverterePub2Settings
    {
        private readonly List<AppleEPub2PlatformSettings> _platforms = new List<AppleEPub2PlatformSettings>();


        [XmlArray("Platforms"), XmlArrayItem(typeof(AppleEPub2PlatformSettings), ElementName = "Platform")]
        public List<AppleEPub2PlatformSettings> Platforms { get { return _platforms; } }


        public void SetupDefaults()
        {
            _platforms.Clear();
            AppleEPub2PlatformSettings platform = new AppleEPub2PlatformSettings { Name = AppleTargetPlatform.All, 
                                                                UseCustomFonts = true, 
                                                                OpenToSpread = false, 
                                                                OrientationLock = AppleOrientationLock.None,
                                                                FixedLayout = false};
            _platforms.Add(platform);
        }

        public void CopyFrom(AppleConverterePub2Settings appleConverterSettings)
        {
            if (appleConverterSettings == null)
            {
                throw new ArgumentNullException("AppleConverterePubV2Settings");
            }
            if (appleConverterSettings == this)
            {
                return;
            }
            _platforms.Clear();
            foreach (var platform in appleConverterSettings._platforms)
            {
                AppleEPub2PlatformSettings platformTo = new AppleEPub2PlatformSettings();
                platformTo.CopyFrom(platform);
                _platforms.Add(platformTo);
            }
        }
    }
}
