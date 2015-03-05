using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EPubLibrary.Content.NavigationDocument
{
    public enum NavigationTableType
    {
        TOC,                // Table of Context
        TOCBrief,           // Brief TOC
        Landmarks,          // Landmarks
        LOA,                // List of Audio Clips
        LOI,                // List of Illustrations
        LOT,                // List of Tables
        LOV,                // List Of videos
    }

    public enum HeadingTypeOptions
    {
        H1,
        H2,
        H3,
        H4,
        H5
    }

    public class NavMapElementV3 : List<NavPointV3>
    {
        public string Name { get; set; }

        public string Heading { get; set; }

        public HeadingTypeOptions HeadingType { get; set; }

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
            var navMap = new XElement(WWWNamespaces.XHTML + "nav");
            string typeAsString = TypeToString(_type);
            navMap.Add(new XAttribute(EPubNamespaces.OpsNamespace + "type", typeAsString));
            navMap.Add(new XAttribute("id", typeAsString)); // use same as id
            navMap.Add(new XAttribute("class",typeAsString));

            if (!string.IsNullOrEmpty(Heading))
            {
                var h1 = new XElement(WWWNamespaces.XHTML + GetHeadingTypeAsString(HeadingType)) { Value = Heading };
                navMap.Add(h1);              
            }

            var subElements = new XElement(WWWNamespaces.XHTML + "ol");
            foreach (var point in this)
            {
                XElement navXPoint = point.Generate();
                subElements.Add(navXPoint);
            }
            navMap.Add(subElements);
            return navMap;
        }

        private string GetHeadingTypeAsString(HeadingTypeOptions headingType)
        {
            switch (headingType)
            {
                case HeadingTypeOptions.H1:
                    return "h1";
                case HeadingTypeOptions.H2:
                    return "h2";
                case HeadingTypeOptions.H3:
                    return "h3";
                case HeadingTypeOptions.H4:
                    return "h4";
                case HeadingTypeOptions.H5:
                    return "h5";
            }
            return "h1";
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
