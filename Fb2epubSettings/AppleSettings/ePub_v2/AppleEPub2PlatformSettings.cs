using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using EPubLibraryContracts.Settings;

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


        #region constants

        public const string PlatformElementName = "Platform";

        private const string UseCusomFontsOnAppleDevicesElementName = "UseCusomFontsOnAppleDevices";
        private const string OpenToSpreadOnAppleDevicesElementName = "OpenToSpreadOnAppleDevices";
        private const string PlatformTypeElementName = "PlatformType";
        private const string FixedLayoutOnAppleDevicesElementName = "FixedLayoutOnAppleDevices";
        private const string OrientationLockOnAppleDevicesElementName = "OrientationLockOnAppleDevices";

        #endregion


        /// <summary>
        /// Direct system to use custom fonts for Apple based systems
        /// </summary>
        public bool UseCustomFonts
        {
            get { return _useCustomFonts; }
            set { _useCustomFonts = value; }
        }

        /// <summary>
        /// Define if book should be opened as one or two pages
        /// </summary>
        public bool OpenToSpread
        {
            get { return _openToSpread; }
            set { _openToSpread = value; }
        }


        /// <summary>
        /// Defines platform , settings to be applied to
        /// </summary>
        public AppleTargetPlatform Name
        {
            get { return _targetPlatform; }
            set { _targetPlatform = value; }
        }

        public bool FixedLayout
        {
            get { return _fixedLayout; }
            set { _fixedLayout = value; }
        }

        /// <summary>
        /// Defines if to lock the book in specific orientation and if 'yes' 
        /// which orientation
        /// </summary>
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (!reader.EOF )
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case UseCusomFontsOnAppleDevicesElementName:
                            _useCustomFonts = reader.ReadElementContentAsBoolean();
                            continue;
                        case OpenToSpreadOnAppleDevicesElementName:
                            _openToSpread = reader.ReadElementContentAsBoolean();
                            continue;
                        case PlatformTypeElementName:
                            AppleTargetPlatform target;
                            string elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out target))
                            {
                                throw new InvalidDataException(string.Format("Invalid target platform {0} specified", elementContent));
                            }
                            _targetPlatform = target;
                            continue;
                        case FixedLayoutOnAppleDevicesElementName:
                            _fixedLayout = reader.ReadElementContentAsBoolean();
                            continue;
                        case OrientationLockOnAppleDevicesElementName:
                            AppleOrientationLock orientationLock;
                            elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out orientationLock))
                            {
                                throw new InvalidDataException(string.Format("Invalid orientation lock {0} specified", elementContent));
                            }
                            _orientationLock = orientationLock;
                            continue;
                    }
                }
                reader.Read();
            }            
            
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(PlatformElementName);

            writer.WriteStartElement(UseCusomFontsOnAppleDevicesElementName);
            writer.WriteValue(_useCustomFonts);
            writer.WriteEndElement();

            writer.WriteStartElement(OpenToSpreadOnAppleDevicesElementName);
            writer.WriteValue(_openToSpread);
            writer.WriteEndElement();

            writer.WriteStartElement(PlatformTypeElementName);
            writer.WriteValue(_targetPlatform.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(FixedLayoutOnAppleDevicesElementName);
            writer.WriteValue(_fixedLayout);
            writer.WriteEndElement();

            writer.WriteStartElement(OrientationLockOnAppleDevicesElementName);
            writer.WriteValue(_orientationLock.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }

}
