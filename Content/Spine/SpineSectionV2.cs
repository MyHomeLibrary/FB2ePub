using System.Collections.Generic;
using System.Xml.Linq;

namespace EPubLibrary.Content.Spine
{
    public class SpineSectionV2 : List<SpineItemV2>
    {
        private readonly XNamespace _opfNameSpace = @"http://www.idpf.org/2007/opf";

        public XElement GenerateSpineElement()
        {
            var spineElement = new XElement(_opfNameSpace + "spine");
            spineElement.Add(new XAttribute("toc", "ncx"));

            foreach (var spineItem in this)
            {
                var itemref = new XElement(_opfNameSpace + "itemref");
                itemref.Add(new XAttribute("idref", spineItem.Name));
                spineElement.Add(itemref);
            }
            return spineElement;
        }
    }
}