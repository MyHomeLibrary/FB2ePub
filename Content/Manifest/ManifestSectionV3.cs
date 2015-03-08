using System.Collections.Generic;
using System.Xml.Linq;
using EPubLibraryContracts.Settings;

namespace EPubLibrary.Content.Manifest
{
    internal class ManifestSectionV3 : List<ManifestItemV3>
    {
        private EPubV3SubStandard _standard;

        public ManifestSectionV3(EPubV3SubStandard standard)
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
