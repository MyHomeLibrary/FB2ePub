using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.Content.Manifest
{
    class ManifestItemV3
    {
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HRef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EPubCoreMediaType MediaType { get; set; }

        // the following are supported only in V3
        public string Fallback { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string MediaOverlay { get; set; }


        /// <summary>
        /// The remote-resources property indicates that the described Publication Resource contains one or more internal references to other Publication Resources that are located outside of the EPUB Container. 
        /// </summary>
        public bool ContainsRemoteResources { get; set; }

        /// <summary>
        /// The cover-image property identifies the described Publication Resource as the cover image for the Publication.
        /// </summary>
        public bool CoverImage { get; set; }

        /// <summary>
        /// The mathml property indicates that the described Publication Resource contains one or more instances of MathML markup.
        /// </summary>
        public bool MathML { get; set; }

        /// <summary>
        /// The nav property indicates that the described Publication Resource constitutes the EPUB Navigation Document of the given Rendition. 
        /// </summary>
        public bool Nav { get; set; }

        /// <summary>
        /// The scripted property indicates that the described Publication Resource is a Scripted Content Document (i.e., contains scripted content and/or elements from HTML5 forms).
        /// </summary>
        public bool Scripted { get; set; }


        /// <summary>
        /// The svg property indicates that the described Publication Resource embeds one or more instances of SVG markup.
        /// </summary>
        public bool SVG { get; set; }


        /// <summary>
        /// The switch property indicates that the described Publication Resource contains one or more instances of the epub:switch element.
        /// </summary>
        public bool Switch { get; set; }

        public XElement GenerateElement()
        {
            var tocElement = new XElement(EPubNamespaces.OpfNameSpace + "item");
            tocElement.Add(new XAttribute("id", ID));
            tocElement.Add(new XAttribute("href", HRef));
            tocElement.Add(new XAttribute("media-type", MediaType.GetAsSerializableString()));
            if (!string.IsNullOrEmpty(Fallback))
            {
                tocElement.Add(new XAttribute("fallback", Fallback));
            }

            List<string> properties;
            BuildPropertiesList(out properties);


            if (properties.Count > 0)
            {
                var sb = new StringBuilder();
                bool first = true;
                foreach (var property in properties)
                {
                    if (!first)
                    {
                        sb.AppendFormat(" {0}", property);
                    }
                    else
                    {
                        sb.AppendFormat("{0}", property);
                        first = false;
                    }
                }
                tocElement.Add(new XAttribute("properties", sb.ToString()));
            }

            if (!string.IsNullOrEmpty(MediaOverlay))
            {
                tocElement.Add(new XAttribute("media-overlay", MediaOverlay));
            }
            return tocElement;
        }

        private void BuildPropertiesList(out List<string> properties)
        {
            properties = new List<string>();

            if (ContainsRemoteResources)
            {
                properties.Add("remote-resources");
            }

            if (CoverImage)
            {
                properties.Add("cover-image");
            }

            if (MathML)
            {
                properties.Add("mathml");
            }

            if (Nav)
            {
                properties.Add("nav");
            }

            if (SVG)
            {
                properties.Add("svg");
            }

            if (Scripted)
            {
                properties.Add("scripted");
            }

            if (Switch)
            {
                properties.Add("switch");
            }
        }

    }
}
