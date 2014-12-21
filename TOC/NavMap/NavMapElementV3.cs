using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EPubLibrary.TOC.NavMap
{
    public enum NavigationTableType
    {
        TOC,
        TOCBrief,
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
            var navMap = new XElement(DaisyNamespaces.NCXNamespace + "nav");
            string typeAsString = TypeToString(_type);
            navMap.Add(new XAttribute(EPubNamespaces.OpsNamespace + "type", typeAsString));
            navMap.Add(new XAttribute("id", typeAsString)); // use same as id

            if (!string.IsNullOrEmpty(NavHeading))
            {
                var h1 = new XElement(DaisyNamespaces.NCXNamespace + "h1") { Value = NavHeading };
                navMap.Add(h1);              
            }

            var subElements = new XElement(DaisyNamespaces.NCXNamespace + "ol");
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
                case NavigationTableType.TOCBrief:
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
