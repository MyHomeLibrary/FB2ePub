using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "br" tag inserts a single line break.  
    /// The "br" tag is an empty tag which means that it has no end tag.
    ///</summary>
    [HTMLItemAttribute(ElementName = "br", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class EmptyLine : HTMLItem, IInlineItem // <br>
    {
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