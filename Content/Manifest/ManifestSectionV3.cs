using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.Content.Manifest
{
    internal class ManifestSectionV3 : List<ManifestItemV3>
    {
        private V3Standard _standard;

        public ManifestSectionV3(V3Standard standard)
        {
            _standard = standard;
        }

        public XElement GenerateManifestElement()
        {
            var manifestElement = new XElement(EPubNamespaces.OpfNameSpace + "manifest");

            foreach (var manifestItem in this)
            {
                var tocElement = new XElement(EPubNamespaces.OpfNameSpace + "item");
                tocElement.Add(new XAttribute("id", manifestItem.ID));
                tocElement.Add(new XAttribute("href", manifestItem.HRef));
                tocElement.Add(new XAttribute("media-type", manifestItem.MediaType.GetAsSerializableString()));
                if (!string.IsNullOrEmpty(manifestItem.Fallback))
                {
                    tocElement.Add(new XAttribute("fallback", manifestItem.Fallback));
                }

                List<string> properties;
                BuildPropertiesList(manifestItem, out properties);


                if (properties.Count > 0)
                {
                    var sb = new StringBuilder();
                    bool first = true;
                    foreach (var property in properties)
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

        private void BuildPropertiesList(ManifestItemV3 manifestItem, out List<string> properties)
        {
            properties = new List<string>();
            
            if (manifestItem.ContainsRemoteResources)
            {
                properties.Add("remote-resources");
            }

            if (manifestItem.CoverImage)
            {
                properties.Add("cover-image");
            }

            if (manifestItem.MathML)
            {
                properties.Add("mathml");
            }

            if (manifestItem.Nav)
            {
                properties.Add("nav");
            }

            if (manifestItem.SVG)
            {
                properties.Add("svg");
            }

            if (manifestItem.Scripted)
            {
                properties.Add("scripted");
            }

            if (manifestItem.Switch)
            {
                properties.Add("switch");
            }
        }

    }
}
