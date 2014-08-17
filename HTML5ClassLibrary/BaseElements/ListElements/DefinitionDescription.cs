using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.ListElements
{
    /// <summary>
    /// The dd element is a definition description for an item in a definition list.
    /// </summary>
    [HTMLItemAttribute(ElementName = "dd", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class DefinitionDescription : HTMLItem 
    {
        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }


        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}
