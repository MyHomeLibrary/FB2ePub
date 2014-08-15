using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "canvas" tag is used to draw graphics, on the fly, via scripting (usually JavaScript).
    /// The "canvas" tag is only a container for graphics, you must use a script to actually draw the graphics.
    /// </summary>
    [HTMLItemAttribute(ElementName = "canvas", SupportedStandards = HTMLElementType.HTML5)]
    public class Canvas : HTMLItem, IBlockElement
    {
        public Canvas()
        {
            RegisterAttribute(_height);
            RegisterAttribute(_width);
        }

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

        public override bool IsValid()
        {
            return true;
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
