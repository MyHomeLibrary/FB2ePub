using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "nav" tag defines a set of navigation links.
    /// Notice that NOT all links of a document should be inside a "nav" element. The "nav" element is intended only for major block of navigation links.
    /// Browsers, such as screen readers for disabled users, can use this element to determine whether to omit the initial rendering of this content.
    /// </summary>
    [HTMLItemAttribute(ElementName = "nav", SupportedStandards = HTMLElementType.HTML5)]
    public class Nav : HTMLItem, IBlockElement
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
