using System;
using System.Xml.Serialization;
using ConverterContracts.Settings;

namespace Fb2epubSettings.AppleSettings.ePub_v2
{
    /// <summary>
    /// Represent Apple settings for ePub v2 files, subset for one "platform" - device type
    /// Based on Apple "iBookStore Assets Guide v4.7 Rev 3"
    /// </summary>
    [Serializable]
    public class AppleEPub2PlatformSettings : IAppleEPub2PlatformSettings
    {
        private bool _useCustomFonts = true;
        private bool _openToSpread;
        private AppleTargetPlatform _targetPlatform = AppleTargetPlatform.NotSet;
        private AppleOrientationLock _orientationLock = AppleOrientationLock.None;
        private bool _fixedLayout;

        /// <summary>
        /// Direct system to use custom fonts for Apple based systems
        /// </summary>
        [XmlElement(ElementName = "UseCusomFontsOnAppleDevices")]
        public bool UseCustomFonts
        {
            get { return _useCustomFonts; }
            set { _useCustomFonts = value; }
        }

        /// <summary>
        /// Define if book should be opened as one or two pages
        /// </summary>
        [XmlElement(ElementName = "OpenToSpreadOnAppleDevices")]
        public bool OpenToSpread
        {
            get { return _openToSpread; }
            set { _openToSpread = value; }
        }


        /// <summary>
        /// Defines platform , settings to be applied to
        /// </summary>
        [XmlAttribute(AttributeName = "PlatformType")]
        public AppleTargetPlatform Name
        {
            get { return _targetPlatform; }
            set { _targetPlatform = value; }
        }

        [XmlElement(ElementName = "FixedLayoutOnAppleDevices")]
        public bool FixedLayout
        {
            get { return _fixedLayout; }
            set { _fixedLayout = value; }
        }

        /// <summary>
        /// Defines if to lock the book in specific orientation and if 'yes' 
        /// which orientation
        /// </summary>
        [XmlElement(ElementName = "OrientationLockOnAppleDevices")]
        public AppleOrientationLock OrientationLock
        {
            get { return _orientationLock; }
            set { _orientationLock = value; }
        }

        /// <summary>
        /// Copies values from another object
        /// </summary>
        /// <param name="otherSettings"></param>
        public void CopyFrom(IAppleEPub2PlatformSettings otherSettings)
        {
            if (otherSettings == null)
            {
                throw new ArgumentNullException("otherSettings");
            }
            if (otherSettings == this)
            {
                return;
            }
            _useCustomFonts = otherSettings.UseCustomFonts;
            _openToSpread = otherSettings.OpenToSpread;
            _targetPlatform = otherSettings.Name;
            _orientationLock = otherSettings.OrientationLock;
            _fixedLayout = otherSettings.FixedLayout;
        }

        public override string ToString()
        {
            return _targetPlatform.ToString();
        }

    }

}
