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
                XElement tocElement = manifestItem.GenerateElement();
                manifestElement.Add(tocElement);
            }

            return manifestElement;
        }
    }
}
