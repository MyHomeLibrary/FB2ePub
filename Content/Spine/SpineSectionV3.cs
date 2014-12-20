using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.Content.Spine
{
    class SpineSectionV3 : List<SpineItemV3>
    {
        private readonly XNamespace _opfNameSpace = @"http://www.idpf.org/2007/opf";
        private V3Standard _standard;

        public SpineSectionV3(V3Standard standard)
        {
            _standard = standard;
        }

        public bool GenerateCompatibleTOC { get; set; }

        public XElement GenerateSpineElement()
        {
            var spineElement = new XElement(_opfNameSpace + "spine");
            if (GenerateCompatibleTOC)
            {
                spineElement.Add(new XAttribute("toc", "ncx"));
            }

            foreach (var spineItem in this)
            {
                var itemref = new XElement(_opfNameSpace + "itemref");
                itemref.Add(new XAttribute("idref", spineItem.Name));
                if (!spineItem.Linear) // true by default so no need to set
                {
                    itemref.Add(new XAttribute("linear", "false"));
                }
                if (!string.IsNullOrEmpty(spineItem.ID))
                {
                    itemref.Add(new XAttribute("id", spineItem.ID));
                }
                if (spineItem.Properties.Count > 0)
                {
                    var sb = new StringBuilder();
                    bool first = true;
                    foreach (var property in spineItem.Properties)
                    {
                        if (!first)
                        {
                            sb.AppendFormat(" {0}", PropertyDescription.GetDescription(property));
                        }
                        else
                        {
                            sb.AppendFormat("{0}", PropertyDescription.GetDescription(property));
                            first = false;
                        }
                    }
                    itemref.Add(new XAttribute("properties", sb.ToString()));
                  
                }
                spineElement.Add(itemref);
            }
            return spineElement;
        }

    }
}
