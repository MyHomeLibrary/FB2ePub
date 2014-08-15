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
    /// The "article" tag specifies independent, self-contained content.
    /// An article should make sense on its own and it should be possible to distribute it independently from the rest of the site.
    /// Potential sources for the "article" element:
    ///     Forum post
    ///     Blog post
    ///     News story
    ///     Comment
    /// </summary>
    [HTMLItemAttribute(ElementName = "article", SupportedStandards = HTMLElementType.HTML5)]
    public class Article : HTMLItem, IBlockElement
    {

        public override bool IsValid()
        {
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
