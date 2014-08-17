using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using XHTMLClassLibrary.BaseElements.BlockElements;
using Script = XHTMLClassLibrary.BaseElements.InlineElements.Script;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The head element contains information about the current document, such as its title, 
    /// keywords that may be useful to search engines, 
    /// and other data that is not considered to be document content. 
    /// This information is usually not displayed by browsers.
    /// </summary>
    [HTMLItemAttribute(ElementName = "head", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Head : HTMLItem
    {

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is Title   || 
                item is Base    ||
                item is Link    ||
                item is Meta    ||
                item is Script  ||
                item is Style   ||
                item is NoScript
                )
            {
                return item.IsValid();
            }
            return false;
        }

        public override bool IsValid()
        {
            return (Subitems.Count(x => x is Title) <= 1);
        }

    }
}
