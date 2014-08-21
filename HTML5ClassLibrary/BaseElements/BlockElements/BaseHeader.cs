using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    public interface IHeader
    {
        
    }

    /// <summary>
    /// The elements h1 to h6 group the contents of a document into sections, and briefly describe the topic of each section. 
    /// There are six levels of headings, h1 being the most important and h6 the least important.
    /// </summary>
    public abstract class BaseHeader : HTMLItem, IBlockElement  , IHeader
    {
        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignTypeAttribute _alignAttribute = new AlignTypeAttribute();


        /// <summary>
        ///  Specifies the alignment of a heading
        /// Not supported in HTML5.
        /// </summary>
        public AlignTypeAttribute Align { get { return _alignAttribute; }}

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
