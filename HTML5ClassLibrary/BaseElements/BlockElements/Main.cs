using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.BaseElements.InlineElements;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "main" tag specifies the main content of a document.
    /// The content inside the "main" element should be unique to the document. It should not contain any content that is repeated across documents such as sidebars, navigation links, copyright information, site logos, and search forms.
    /// Note: There must not be more than one "main" element in a document. The "main" element must NOT be a descendent of an "article", "aside", "footer", "header", or "nav" element.
    /// </summary>
    public class Main : BaseBlockElement
    {
        public const string ElementName = "main";

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
