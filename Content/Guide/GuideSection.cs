using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EPubLibrary.Content.Guide
{
    internal class GuideSection
    {
        private readonly XNamespace opfNameSpace = @"http://www.idpf.org/2007/opf";

        private readonly List<GuideElement> content = new List<GuideElement>();

        public XElement GenerateGuide()
        {
            XElement xGuide = new XElement(opfNameSpace + "guide");
            foreach (var guideElement in content)
            {
                if (guideElement.IsValid())
                {
                    XElement xRef = new XElement(opfNameSpace + "reference");
                    xRef.Add(new XAttribute("type",guideElement.GetTypeAsString()));
                    xRef.Add(new XAttribute("title",guideElement.Title));
                    xRef.Add(new XAttribute("href",guideElement.Link));
                    xGuide.Add(xRef);
                }
            }
            return xGuide;
        }

        public void AddGuideItem(string link, string title, GuideTypeEnum type)
        {
            GuideElement element = new GuideElement { Link = link.Replace('\\', '/'), Title = title, Type = type };

            content.Add(element);
        }

        public bool HasData()
        {
            return (content.Count>0);
        }
    }
}
