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
    /// The "main" tag specifies the main content of a document.
    /// The content inside the "main" element should be unique to the document. It should not contain any content that is repeated across documents such as sidebars, navigation links, copyright information, site logos, and search forms.
    /// Note: There must not be more than one "main" element in a document. The "main" element must NOT be a descendent of an "article", "aside", "footer", "header", or "nav" element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "main", SupportedStandards = HTMLElementType.HTML5)]
    public class Main :  HTMLItem, IBlockElement
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
