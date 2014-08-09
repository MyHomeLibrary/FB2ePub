using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    public class EmptyLine : BaseInlineItem // <br>
    {
        public const string ElementName = "br";

        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement) xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception("xNode is not empty line element");
            }
            ReadAttributes(xElement);
        }

        public override XNode Generate()
        {
            var xElement =  new XElement(XhtmlNameSpace + ElementName);
            AddAttributes(xElement);
            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }
    }
}