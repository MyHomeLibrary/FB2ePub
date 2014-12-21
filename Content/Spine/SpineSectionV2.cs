using System.Collections.Generic;
using System.Xml.Linq;

namespace EPubLibrary.Content.Spine
{
    public class SpineSectionV2 : List<SpineItemV2>
    {
        public XElement GenerateSpineElement()
        {
            var spineElement = new XElement(EPubNamespaces.OpfNameSpace + "spine");
            spineElement.Add(new XAttribute("toc", "ncx"));

            foreach (var spineItem in this)
            {
                var itemref = new XElement(EPubNamespaces.OpfNameSpace + "itemref");
                itemref.Add(new XAttribute("idref", spineItem.Name));
                spineElement.Add(itemref);
            }
            return spineElement;
        }
    }
}