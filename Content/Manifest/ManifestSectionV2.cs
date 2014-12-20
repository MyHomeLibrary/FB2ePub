using System.Collections.Generic;
using System.Xml.Linq;

namespace EPubLibrary.Content.Manifest
{
    public class ManifestSectionV2 : List<ManifestItemV2>
    {
        protected readonly XNamespace OpfNameSpace = @"http://www.idpf.org/2007/opf";

        public XElement GenerateManifestElement()
        {
            var manifestElement = new XElement(OpfNameSpace + "manifest");

            foreach (var manifestItem in this)
            {
                var tocElement = new XElement(OpfNameSpace + "item");
                tocElement.Add(new XAttribute("id", manifestItem.ID));
                tocElement.Add(new XAttribute("href", manifestItem.HRef));
                tocElement.Add(new XAttribute("media-type", manifestItem.MediaType.GetAsSerializableString()));
                manifestElement.Add(tocElement);
            }

            return manifestElement;
        }
    }
}