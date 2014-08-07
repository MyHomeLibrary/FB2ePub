using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.Content.Manifest
{
    internal class ManifestSectionV3 : ManifestSection
    {
        private V3Standard _standard;

        public ManifestSectionV3(V3Standard standard)
        {
            _standard = standard;
        }

        public override XElement GenerateManifestElement()
        {
            XElement manifestElement = new XElement(_opfNameSpace + "manifest");

            foreach (var manifestItem in this)
            {
                XElement tocElement = new XElement(_opfNameSpace + "item");
                tocElement.Add(new XAttribute("id", manifestItem.ID));
                tocElement.Add(new XAttribute("href", manifestItem.HRef));
                tocElement.Add(new XAttribute("media-type", manifestItem.MediaType));
                if (!string.IsNullOrEmpty(manifestItem.Fallback))
                {
                    tocElement.Add(new XAttribute("fallback", manifestItem.Fallback));
                }
                if (manifestItem.Properties.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    bool first = true;
                    foreach (var property in manifestItem.Properties)
                    {
                        if (!first)
                        {
                            sb.AppendFormat(" {0}",property);
                        }
                        else
                        {
                            sb.AppendFormat("{0}",property);
                            first = false;
                        }                       
                    }
                    tocElement.Add(new XAttribute("properties", sb.ToString()));
                }

                if (!string.IsNullOrEmpty(manifestItem.MediaOverlay))
                {
                    tocElement.Add(new XAttribute("media-overlay", manifestItem.MediaOverlay));
                }

                manifestElement.Add(tocElement);
            }

            return manifestElement;
        }

    }
}
