using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The hr element is used to separate sections of content. 
    /// Though the name of the hr element is "horizontal rule", most visual Web browsers render hr as a horizontal line.
    /// </summary>
    [HTMLItemAttribute(ElementName = "hr", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class HorizontalRuler : HTMLItem, IBlockElement 
    {
        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public override bool IsValid()
        {
            return true;
        }


        public override List<IHTMLItem> SubElements()
        {
            return null;
        }

    }
}
