using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The div element offers a generic way of grouping areas of content.
    /// </summary>
    [HTMLItemAttribute(ElementName = "div", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Div : HTMLItem, IBlockElement
    {

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

        public override bool IsValid()
        {
            return true;
        }

    }
}
