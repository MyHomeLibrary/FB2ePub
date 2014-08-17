using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Exceptions;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The tbody element can be used to group table data rows. 
    /// This can be useful when a Web browser supports scrolling of table rows in longer tables. 
    /// Multiple tbody elements can be used for independent scrolling.
    /// </summary>
    [HTMLItemAttribute(ElementName = "tbody", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]    
    public class TableBody : HTMLItem
    {
        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is TableRow)
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
