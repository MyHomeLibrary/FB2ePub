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
        private readonly List<NavPointV3> _subpoints = new List<NavPointV3>();

        public List<NavPointV3> SubPoints { get { return _subpoints; } }
        public string Name { get; set; }
        public string Content { set; get; }
        public string Id { get; set; }

        public int GetDepth()
        {
            int depth = 1;
            if (_subpoints.Count > 0)
            {
                depth += _subpoints.Max(x => x.GetDepth());
            }
            return depth;
        }

        public List<NavPointV3> AllContent()
        {
            var resList = new List<NavPointV3>();

            resList.AddRange(_subpoints);

            foreach (var subpoint in _subpoints)
            {
                resList.AddRange(subpoint.AllContent());
            }
            return resList;
        }

        internal void RemoveDeadEnds()
        {
            var listToDelete = new List<NavPointV3>();
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
            var navXPoint = new XElement(WWWNamespaces.XHTML + "li");
            navXPoint.Add(new XAttribute("id", Id));
            var link = new XElement(WWWNamespaces.XHTML + "a") {Value = Name};
            link.Add(new XAttribute("href",Content));
            navXPoint.Add(link);
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
                var subElements = new XElement(WWWNamespaces.XHTML + "ol");
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
