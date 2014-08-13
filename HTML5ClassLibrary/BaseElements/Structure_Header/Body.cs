using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

using HTML5ClassLibrary.BaseElements.BlockElements;

namespace HTML5ClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The body element contains the contents of a Web page.
    /// </summary>
    public class Body : BaseContainingElement
    {
        public const string ElementName = "body";


        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

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
            // body in epub can't be empty
            return (Content.Count > 0);
        }

        protected override bool IsValidSubType(IHTML5Item item)
        {
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
