using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace EPubLibrary.Content.NavigationDocument
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

        internal XElement Generate()
        {
            var navXPoint = new XElement(WWWNamespaces.XHTML + "li");
            navXPoint.Add(new XAttribute("id", Id));
            var link = new XElement(WWWNamespaces.XHTML + "a") { Value = EnsureValid(Name) };
            link.Add(new XAttribute("href",Content));
            navXPoint.Add(link);

            if (SubPoints.Count > 0) // if has sub items ("branch")
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
        /// <param name="name"></param>
        /// <returns></returns>
        private static string EnsureValid(string name)
        {
            var localName = name;
            if (string.IsNullOrEmpty(localName))
            {
                localName = " --- ";
            }
            return WebUtility.HtmlEncode(localName);
        }
 
    }
}
