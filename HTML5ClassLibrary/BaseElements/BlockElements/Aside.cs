using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "aside" tag defines some content aside from the content it is placed in.
    /// The aside content should be related to the surrounding content.
    /// </summary>
    [HTMLItemAttribute(ElementName = "aside", SupportedStandards = HTMLElementType.HTML5)]
    public class Aside : HTMLItem, IBlockElement
    {

        public override bool IsValid()
        {
            foreach (var item in Subitems)
            {
                if (!item.IsValid())
                {
                    return false;
                }
            }
            return true;
        }

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

    }
}
