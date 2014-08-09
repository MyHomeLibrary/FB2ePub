using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.BaseElements.InlineElements;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "header>" tag specifies a header for a document or section.
    /// The "header>" element should be used as a container for introductory content or set of navigational links.
    /// You can have several "header>" elements in one document. 
    /// Note: A "header>" tag cannot be placed within a "footer", "address" or another "header" element.
    /// </summary>
    public class Header : BaseBlockElement
    {
        public const string ElementName = "header";

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

            Content.Clear();
            IEnumerable<XNode> descendants = xElement.Nodes();
            foreach (var node in descendants)
            {
                IHTML5Item item = ElementFactory.CreateElement(node);
                if ((item != null) && IsValidSubType(item))
                {
                    try
                    {
                        item.Load(node);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    Content.Add(item);
                }
            }

        }

        protected override bool IsValidSubType(IHTML5Item item)
        {
            if (item is Header)
            {
                return false;
            }
            if (item is Footer)
            {
                return false;
            }
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            foreach (var item in Content)
            {
                xElement.Add(item.Generate());
            }
            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

    }
}
