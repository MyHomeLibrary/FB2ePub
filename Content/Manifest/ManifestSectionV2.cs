using System.Collections.Generic;
using System.Xml.Linq;
using EPubLibraryContracts;

namespace EPubLibrary.Content.Manifest
{
    public class ManifestSectionV2 : List<ManifestItemV2>
    {
        public XElement GenerateManifestElement()
        {
            var manifestElement = new XElement(EPubNamespaces.OpfNameSpace + "manifest");

            foreach (var manifestItem in this)
            {
                var tocElement = new XElement(EPubNamespaces.OpfNameSpace + "item");
                tocElement.Add(new XAttribute("id", manifestItem.ID));
                tocElement.Add(new XAttribute("href", manifestItem.HRef));
                tocElement.Add(new XAttribute("media-type", manifestItem.MediaType.GetAsSerializableString()));
                manifestElement.Add(tocElement);
            }

            return manifestElement;
        }
    }
}