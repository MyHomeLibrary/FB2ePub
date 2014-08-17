using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.Ruby
{
    /// <summary>
    /// Ruby is mechanism for adding annotations to characters of East Asian languages such as Chinese and Japanese. 
    /// These annotations typically appear in smaller typeface above or to the side of regular text, 
    /// and are meant to help with pronunciation of obscure characters or as a language learning aid.
    /// </summary>
    [HTMLItemAttribute(ElementName = "ruby", SupportedStandards = HTMLElementType.HTML5)]
    public class RubyElement : HTMLItem, IInlineItem
    {
        protected override bool IsValidSubType(IHTMLItem item)
        {
            // TODO: full check for ruby sequence
            if (item is IRubyItem)
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
