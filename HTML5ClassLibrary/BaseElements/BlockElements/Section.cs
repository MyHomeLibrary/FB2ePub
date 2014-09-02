using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "section" tag defines sections in a document, such as chapters, headers, footers, or any other sections of the document.
    /// </summary>
    [HTMLItemAttribute(ElementName = "section", SupportedStandards = HTMLElementType.HTML5)]
    public class Section : HTMLItem, IBlockElement 
    {
        public Section(HTMLElementType htmlStandard) : base(htmlStandard)
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
