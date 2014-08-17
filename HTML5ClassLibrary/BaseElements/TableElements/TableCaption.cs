using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The caption element creates a caption for a table. 
    /// If a caption is to be used, it should be the first element after the opening table element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "caption", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class TableCaption : HTMLItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignAttribute _alignAttribute = new BiDirectionalAlignAttribute();


        /// <summary>
        /// Defines the alignment of the caption
        /// Not supported in HTML5
        /// </summary>
        public BiDirectionalAlignAttribute Align { get { return _alignAttribute; }}

        protected override bool IsValidSubType(IHTMLItem item)
        {
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
