using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.Content.Manifest
{
    internal class ManifestSection : List<ManifestItem>
    {
        private readonly XNamespace opfNameSpace = @"http://www.idpf.org/2007/opf";

        public XElement GenerateManifestElement()
        {
            XElement manifestElement = new XElement(opfNameSpace + "manifest");

            foreach (var manifestItem in this)
            {
                XElement tocElement = new XElement(opfNameSpace + "item");
                tocElement.Add(new XAttribute("id", manifestItem.ID));
                tocElement.Add(new XAttribute("href", manifestItem.HRef));
                tocElement.Add(new XAttribute("media-type", manifestItem.MediaType));
                manifestElement.Add(tocElement);
            }

            return manifestElement;
        }
    }
}