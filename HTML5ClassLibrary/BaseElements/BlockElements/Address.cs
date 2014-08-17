using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The address element is used to supply contact information. 
    /// This element often appears at the beginning or end of a document.
    /// </summary>
    [HTMLItemAttribute(ElementName = "address", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Address : HTMLItem, IBlockElement 
    {
        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is Header)
            {
                return false;
            }
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
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
