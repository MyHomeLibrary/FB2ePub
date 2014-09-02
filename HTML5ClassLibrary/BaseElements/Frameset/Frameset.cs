using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.Frameset
{
    /// <summary>
    /// The "frameset" tag defines a frameset.
    ///The "frameset" element holds one or more "frame" elements. Each "frame" element can hold a separate document.
    ///The "frameset" element specifies HOW MANY columns or rows there will be in the frameset, and HOW MUCH percentage/pixels of space will occupy each of them.
    ///Note: If you want to validate a page containing frames, be sure the <!DOCTYPE> is set to either "HTML Frameset DTD" or "XHTML Frameset DTD".
    /// </summary>
    [HTMLItem(ElementName = "frameset", SupportedStandards = HTMLElementType.FrameSet)]
    public class Frameset : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _colsAttribute = new SimpleSingleTypeAttribute<Number>("cols");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _rowsAttribute = new SimpleSingleTypeAttribute<Number>("rows");


        public Frameset(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies the number and size of columns in a frameset
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Cols { get { return _colsAttribute; }}


        /// <summary>
        /// Specifies the number and size of rows in a frameset
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Rows { get { return _rowsAttribute; }}


        public override bool IsValid()
        {
            return true;
        }


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is Frame)
            {
                return true;
            }
            return false;
        }
    }
}
