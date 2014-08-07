using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.TOC.NavMap
{
    public class NavMapElement : List<NavPoint>
    {
        public string Name { get; set; }

        public int GetDepth()
        {
            if ( Count == 0)
            {
                return 1;
            }
            return this.Max(x => x.GetDepth());
        }

        public XElement GenerateXMLMap()
        {
            XElement navMap = new XElement(NavPoint.ncxNamespace + "navMap");
            int pointnumber = 1;
            foreach (var point in this)
            {
                XElement navXPoint  = point.Generate(ref pointnumber);
                navMap.Add(navXPoint);
            }
            return navMap;
        }

    }
}