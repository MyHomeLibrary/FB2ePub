using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EPubLibrary.TOC.NavMap
{
    public class NavPoint 
    {
        private readonly List<NavPoint> _subpoints = new List<NavPoint>();
        
        public List<NavPoint> SubPoints { get { return _subpoints; } }
        public string Name { get; set; }
        public string Content { set; get; }

        public int GetDepth()
        {
            int depth = 1;
            if (_subpoints.Count > 0)
            {   
                depth += _subpoints.Max(x => x.GetDepth());
            }
            return depth;
        }

        public List<NavPoint> AllContent()
        {
            var resList = new List<NavPoint>();

            resList.AddRange(_subpoints);

            foreach (var subpoint in _subpoints)
            {           
                resList.AddRange(subpoint.AllContent());
            }
            return resList;
        }

        internal void RemoveDeadEnds()
        {
            var listToDelete = new List<NavPoint>();
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
            var navXPoint = new XElement(DaisyNamespaces.NCXNamespace + "navPoint");
            navXPoint.Add(new XAttribute("id", string.Format("NavPoint-{0}", pointnumber)));
            navXPoint.Add(new XAttribute("playOrder", pointnumber));

            var navLabel = new XElement(DaisyNamespaces.NCXNamespace + "navLabel");
            var text = new XElement(DaisyNamespaces.NCXNamespace + "text", EnsureValid(Name));
            navLabel.Add(text);
            navXPoint.Add(navLabel);

            var content = new XElement(DaisyNamespaces.NCXNamespace + "content");
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
        /// <param name="name"></param>
        /// <returns></returns>
        private static string EnsureValid(string name)
        {
            return Regex.Replace(name, @"[\x00-\x1f]", string.Empty).Trim();
        }
    }
}