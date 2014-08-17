using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.Legends
{
    /// <summary>
    /// The legend element is a caption to a fieldset element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "legend", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]    
    public class Legend : HTMLItem
    {

        protected override bool IsValidSubType(IHTMLItem item)
        {
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
