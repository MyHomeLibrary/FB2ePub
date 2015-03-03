using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using EPubLibraryContracts.Settings;

namespace Fb2epubSettings.AppleSettings.ePub_v2
{

    /// <summary>
    /// 
    /// </summary>
    public class AppleConverterePub2Settings : IAppleConverterePub2Settings
    {
        private readonly List<IAppleEPub2PlatformSettings> _platforms = new List<IAppleEPub2PlatformSettings>();


        #region constants

        public const string AppleConverterEPub2SettingsElementName = "AppleConverterEPub2Settings";

        private const string PlatformsElementName = "Platforms";

        #endregion


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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case PlatformsElementName:
                            ReadPlatforms(reader.ReadSubtree());
                            break;
                    }
                }
                reader.Read();
            }            
            
        }

        private void ReadPlatforms(XmlReader reader)
        {
            _platforms.Clear();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case AppleEPub2PlatformSettings.PlatformElementName:
                            var platformSettings = new AppleEPub2PlatformSettings();
                            platformSettings.ReadXml(reader.ReadSubtree());
                            _platforms.Add(platformSettings);
                            break;
                    }
                }
            }            
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(AppleConverterEPub2SettingsElementName);

            foreach (var appleEPub2PlatformSettings in _platforms)
            {
                appleEPub2PlatformSettings.WriteXml(writer);
            }

            writer.WriteEndElement();
        }
    }
}
