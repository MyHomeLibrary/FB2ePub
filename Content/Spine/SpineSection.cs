using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.Content.Spine
{
    public class SpineSection : List<SpineItem>
    {
        protected readonly XNamespace _opfNameSpace = @"http://www.idpf.org/2007/opf";

        public virtual XElement GenerateSpineElement()
        {
            XElement spineElement = new XElement(_opfNameSpace + "spine");
            spineElement.Add(new XAttribute("toc", "ncx"));

            foreach (var spineItem in this)
            {
                XElement itemref = new XElement(_opfNameSpace + "itemref");
                itemref.Add(new XAttribute("idref", spineItem.Name));
                spineElement.Add(itemref);
            }
            return spineElement;
        }
    }
}