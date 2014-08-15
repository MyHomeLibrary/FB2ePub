using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "header>" tag specifies a header for a document or section.
    /// The "header>" element should be used as a container for introductory content or set of navigational links.
    /// You can have several "header>" elements in one document. 
    /// Note: A "header>" tag cannot be placed within a "footer", "address" or another "header" element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "header", SupportedStandards = HTMLElementType.HTML5)]    
    public class Header : HTMLItem, IBlockElement
    {
        protected override bool IsValidSubType(IHTMLItem item)
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

        public override bool IsValid()
        {
            return true;
        }

    }
}
