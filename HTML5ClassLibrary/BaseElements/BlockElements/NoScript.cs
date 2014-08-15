using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The noscript element allows authors to provide alternate content when a script is not executed. 
    /// This can be because the Web browser is configured not to process scripts, or because the given script language is not supported.
    /// </summary>
    [HTMLItemAttribute(ElementName = "noscript", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class NoScript : HTMLItem, IBlockElement 
    {

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IBlockElement)
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
