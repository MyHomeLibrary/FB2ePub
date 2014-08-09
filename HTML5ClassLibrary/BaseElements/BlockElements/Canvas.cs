using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.BaseElements.InlineElements;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "canvas" tag is used to draw graphics, on the fly, via scripting (usually JavaScript).
    /// The "canvas" tag is only a container for graphics, you must use a script to actually draw the graphics.
    /// </summary>
    public class Canvas : BaseBlockElement
    {
        public const string ElementName = "canvas";

        private readonly HeightAttribute _height = new HeightAttribute();
        private readonly WidthAttribute _width = new WidthAttribute();


        public HeightAttribute Height
        {
            get { return _height; }
        }

        public WidthAttribute Width
        {
            get { return _width; }
        }

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
            return true;
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

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _height.AddAttribute(xElement);
            _width.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _height.ReadAttribute(xElement);
            _width.ReadAttribute(xElement);
        }
    }
}
