using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    public class Embed : BaseInlineItem
    {
        public const string ElementName = "embed";


        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }
            ReadAttributes(xElement);
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);
            AddAttributes(xElement);
            return xElement;           
        }

        public override bool IsValid()
        {
            return true;
        }

        public override void Add(IHTML5Item item)
        {
            throw new HTML5ViolationException("Embed element can not have children");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new HTML5ViolationException("Embed element can not have children");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }
    }
}
