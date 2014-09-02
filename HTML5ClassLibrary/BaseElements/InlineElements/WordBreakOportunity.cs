using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "wbr" (Word Break Opportunity) tag specifies where in a text it would be ok to add a line-break.
    /// Tip: When a word is too long, or you are afraid that the browser will break your lines at the wrong place, you can use the "wbr" element to add word break opportunities.
    /// </summary>
    [HTMLItemAttribute(ElementName = "wbr", SupportedStandards = HTMLElementType.HTML5)]
    public class WordBreakOportunity : HTMLItem, IInlineItem
    {
        public WordBreakOportunity(HTMLElementType htmlStandard)
            : base(htmlStandard)
        {
            TextContent = new SimpleHTML5Text(htmlStandard);
        }

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
