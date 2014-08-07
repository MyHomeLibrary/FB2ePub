using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.TOC.NavMap
{
    public enum NavigationTableType
    {
        TOC,
        TOC_Brief,
        Landmarks,
        LOA,
        LOI,
        LOT,LOV,
    }

    public class NavMapElementV3 : List<NavPointV3>
    {
        public string Name { get; set; }

        public string NavHeading { get; set; }

        private NavigationTableType _type = NavigationTableType.TOC;

        public NavigationTableType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int GetDepth()
        {
            if (Count == 0)
            {
                return 1;
            }
            return this.Max(x => x.GetDepth());
        }

        public XElement GenerateXMLMap()
        {
            XNamespace ePubNamespace = @"http://www.idpf.org/2007/ops";

            XElement navMap = new XElement(NavPointV3.xmlNamespace + "nav");
            string typeAsString = TypeToString(_type);
            navMap.Add(new XAttribute(ePubNamespace + "type", typeAsString));
            navMap.Add(new XAttribute("id", typeAsString)); // use same as id

            if (!string.IsNullOrEmpty(NavHeading))
            {
                XElement h1 = new XElement(NavPointV3.xmlNamespace + "h1");
                h1.Value = NavHeading;
                navMap.Add(h1);              
            }

            XElement subElements = new XElement(NavPointV3.xmlNamespace + "ol");
            foreach (var point in this)
            {
                XElement navXPoint = point.Generate();
                subElements.Add(navXPoint);
            }
            navMap.Add(subElements);
            return navMap;
        }

        private static string TypeToString(NavigationTableType type)
        {
            switch (type)
            {
                case NavigationTableType.TOC:
                    return "toc";
                case NavigationTableType.TOC_Brief:
                    return "toc-brief";
                case NavigationTableType.Landmarks:
                    return "landmarks";
                case NavigationTableType.LOA:
                    return "loa";
                case NavigationTableType.LOI:
                    return "loi";
                case NavigationTableType.LOT:
                    return "lot";
                case NavigationTableType.LOV:
                    return "lov";
            }
            throw new ArgumentException("Unknown navigation table type","type");
        }

    }
}
