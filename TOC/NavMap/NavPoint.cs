using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EPubLibrary.TOC.NavMap
{
    class NavPoint 
    {
        private List<NavPoint> subpoints = new List<NavPoint>();
        
        internal static XNamespace ncxNamespace = @"http://www.daisy.org/z3986/2005/ncx/";

        public List<NavPoint> SubPoints { get { return subpoints; } }
        public string Name { get; set; }
        public string Content { set; get; }

        public int GetDepth()
        {
            int depth = 1;
            if (subpoints.Count > 0)
            {   
                depth += subpoints.Max(x => x.GetDepth());
            }
            return depth;
        }

        public List<NavPoint> AllContent()
        {
            List<NavPoint> resList = new List<NavPoint>();

            resList.AddRange(subpoints);

            foreach (var subpoint in subpoints)
            {           
                resList.AddRange(subpoint.AllContent());
            }
            return resList;
        }

        internal void RemoveDeadEnds()
        {
            List<NavPoint> listToDelete = new List<NavPoint>();
            foreach (var point in SubPoints)
            {
                if (point.SubPoints.Count == 0 && string.IsNullOrEmpty(point.Name))
                {
                    listToDelete.Add(point);
                }
                else
                {
                    point.RemoveDeadEnds();
                }
            }
            foreach (var toDelete in listToDelete)
            {
                SubPoints.Remove(toDelete);
            }
        }

        internal XElement Generate(ref int pointnumber)
        {
            XElement navXPoint = new XElement(ncxNamespace + "navPoint");
            navXPoint.Add(new XAttribute("id", string.Format("NavPoint-{0}", pointnumber)));
            navXPoint.Add(new XAttribute("playOrder", pointnumber));

            XElement navLabel = new XElement(ncxNamespace + "navLabel");
            XElement text = new XElement(ncxNamespace + "text", EnsureValid(Name));
            navLabel.Add(text);
            navXPoint.Add(navLabel);

            XElement content = new XElement(ncxNamespace + "content");
            content.Add(new XAttribute("src", Content));
            navXPoint.Add(content);

            pointnumber++;

            foreach (var subPoint in SubPoints)
            {
                XElement navSubXPoint = subPoint.Generate(ref pointnumber);
                navXPoint.Add(navSubXPoint);
            }

            return navXPoint;
        }

        /// <summary>
        /// Makes sure that the Name does not contains invalid 
        /// (control) characters that might confuse the reader (ADE etc)
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private static string EnsureValid(string Name)
        {
            return Regex.Replace(Name, @"[\x00-\x1f]", string.Empty).Trim();
        }
    }
}