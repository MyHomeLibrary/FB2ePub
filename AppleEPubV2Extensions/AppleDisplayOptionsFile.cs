using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.PathUtils;

namespace EPubLibrary.AppleEPubV2Extensions
{
    public class AppleDisplayOptionsFile : IEPubPath
    {
        private readonly Dictionary<PlatformType, AppleTargetPlatform> _platforms = new Dictionary<PlatformType, AppleTargetPlatform>();

        public static readonly EPubInternalPath DefaultAppleFilePath = new EPubInternalPath("META-INF/com.apple.ibooks.display-options.xml")
        {
            SupportFlatStructure = false
        };


        public void Write(Stream s)
        {
            XDocument contentDocument = new XDocument();
            CreateContentDocument(contentDocument);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.Unicode;
            settings.Indent = true;
            using (var writer = XmlWriter.Create(s, settings))
            {
                contentDocument.WriteTo(writer);
            }

        }

        private void CreateContentDocument(XDocument contentDocument)
        {
            XElement dispOptions = new XElement("display_options");
            XElement platformXml = new XElement("platform");
            foreach (var platform in _platforms)
            {
                platformXml.Add(new XAttribute("name", AppleTargetPlatform.ConvertTypeToString(platform.Key)));

                XElement option = new XElement("option");
                option.Value = platform.Value.CustomFontsAllowed ? "true" : "false";
                option.Add(new XAttribute("name", "specified-fonts"));
                platformXml.Add(option);

                option = new XElement("option");
                option.Value = platform.Value.FixedLayout ? "true" : "false";
                option.Add(new XAttribute("name", "fixed-layout"));
                platformXml.Add(option);

                option = new XElement("option");
                option.Value = platform.Value.OpenToSpread ? "true" : "false";
                option.Add(new XAttribute("name", "open-to-spread"));
                platformXml.Add(option);

                option = new XElement("option");
                string lockType = "none";
                if (platform.Value.OrientationLockType == OrientationLock.LandscapeOnly)
                {
                    lockType = "landscape-only";
                }
                else if (platform.Value.OrientationLockType == OrientationLock.PortraitOnly)
                {
                    lockType = "portrait-only";
                }
                option.Value = lockType;
                option.Add(new XAttribute("name", "orientation-lock"));
                platformXml.Add(option);

                dispOptions.Add(platformXml);
            }
            contentDocument.Add(dispOptions);            
        }


        public void Reset()
        {
            _platforms.Clear();
        }

        public void AddPlatform(AppleEPubV2Extensions.AppleTargetPlatform targetPlatform)
        {
            if (_platforms.ContainsKey(targetPlatform.Type))
            {
                _platforms.Remove(targetPlatform.Type);
            }
            _platforms.Add(targetPlatform.Type, targetPlatform);
        }

        public EPubInternalPath PathInEPUB
        {
            get
            {
                return DefaultAppleFilePath;
            }
           
        }
    }
}
