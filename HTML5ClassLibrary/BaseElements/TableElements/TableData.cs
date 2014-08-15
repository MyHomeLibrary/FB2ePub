using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.BaseElements.BlockElements;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The td element defines a data cell in a table (i.e. cells that are not header cells).
    /// </summary>
    [HTMLItemAttribute(ElementName = "td", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class TableData : HTMLItem
    {
        public TableData()
        {
            RegisterAttribute(_colSpanAttribute);
            RegisterAttribute(_rowSpanAttribue);
            RegisterAttribute(_headersAttribute);
        }
        private readonly ColSpanAttribute _colSpanAttribute = new ColSpanAttribute();
        private readonly RowSpanAttribute _rowSpanAttribue = new RowSpanAttribute();
        private readonly HeadersAttribute _headersAttribute = new HeadersAttribute();




        /// <summary>
        /// This attribute specifies the number of columns spanned by the current cell.
        /// </summary>
        public ColSpanAttribute ColSpan { get { return _colSpanAttribute; } }


        /// <summary>
        /// This attribute specifies the number of rows spanned by the current cell.
        /// </summary>
        public RowSpanAttribute RowSpan { get { return _rowSpanAttribue; } }


        /// <summary>
        /// Specifies one or more header cells a cell is related to
        /// </summary>
        public HeadersAttribute Headers { get { return _headersAttribute; }}

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            if (item is IBlockElement)
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
