using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EPubLibrary.TOC.NavMap
{
    public class NavPointV3
    {
        private List<NavPointV3> subpoints = new List<NavPointV3>();

        internal static XNamespace xmlNamespace = @"http://www.w3.org/1999/xhtml";

        public List<NavPointV3> SubPoints { get { return subpoints; } }
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

        public List<NavPointV3> AllContent()
        {
            List<NavPointV3> resList = new List<NavPointV3>();

            resList.AddRange(subpoints);

            foreach (var subpoint in subpoints)
            {
                resList.AddRange(subpoint.AllContent());
            }
            return resList;
        }

        internal void RemoveDeadEnds()
        {
            List<NavPointV3> listToDelete = new List<NavPointV3>();
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

        internal XElement Generate()
        {
            XElement navXPoint = new XElement(xmlNamespace + "li");
            //navXPoint.Add(new XAttribute("id", string.Format("NavPoint-{0}", pointnumber)));
            //navXPoint.Add(new XAttribute("playOrder", pointnumber));

            //XElement navLabel = new XElement(xmlNamespace + "navLabel");
            //XElement text = new XElement(xmlNamespace + "text", EnsureValid(Name));
            //navLabel.Add(text);
            //navXPoint.Add(navLabel);

            //XElement content = new XElement(xmlNamespace + "content");
            //content.Add(new XAttribute("src", Content));
            //navXPoint.Add(content);

            if (SubPoints.Count > 0)
            {
                XElement subElements = new XElement(xmlNamespace + "ol");
                foreach (var subPoint in SubPoints)
                {
                    XElement navSubXPoint = subPoint.Generate();
                    subElements.Add(navSubXPoint);
                }
                navXPoint.Add(subElements);
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
            return WebUtility.HtmlEncode(Name);
        }
 
    }
}
