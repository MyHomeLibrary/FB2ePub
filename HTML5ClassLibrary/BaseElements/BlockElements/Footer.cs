using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "footer" tag defines a footer for a document or section.
    ///A "footer" element should contain information about its containing element.
    ///A footer typically contains the author of the document, copyright information, links to terms of use, contact information, etc.
    ///You can have several "footer" elements in one document.
    /// </summary>
    [HTMLItemAttribute(ElementName = "footer", SupportedStandards = HTMLElementType.HTML5)]
    public class Footer : HTMLItem, IBlockElement
    {
        public Footer(HTMLElementType htmlStandard) : base(htmlStandard)
        {
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

        public override bool IsValid()
        {
            return true;
        }

    }
}
