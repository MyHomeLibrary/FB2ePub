using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.BaseElements.BlockElements;
using HTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;

namespace HTMLClassLibrary.BaseElements.ListElements
{
    /// <summary>
    /// The ul element is used to create unordered lists. 
    /// An unordered list is a grouping of items whose sequence in the list is not important. 
    /// For example, the order in which ingredients for a recipe are presented will not affect the outcome of the recipe.
    /// </summary>
    [HTMLItemAttribute(ElementName = "ul", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class UnorderedList : HTMLItem, IBlockElement
    {
        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is ListItem)
            {
                return item.IsValid();
            }
            return false;
        }

        public override bool IsValid()
        {
            return (Subitems.Count > 0);
        }
    }
}
