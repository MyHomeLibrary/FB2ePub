using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The th element defines a table header cell.
    /// </summary>
    [HTMLItemAttribute(ElementName = "th", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class TableHeaderCell : HTMLItem
    {
        public TableHeaderCell()
        {
            RegisterAttribute(_abbrAttribute);
            RegisterAttribute(_colSpanAttribute);
            RegisterAttribute(_headersAttribute);
            RegisterAttribute(_rowSpanAttribue);
            RegisterAttribute(_scopeAttribute);
            RegisterAttribute(_sortedAttribute);
        }

        // Basic attributes
        private readonly AbbreviatedAttribute _abbrAttribute = new AbbreviatedAttribute();
        private readonly ColSpanAttribute _colSpanAttribute = new ColSpanAttribute();
        private readonly HeadersAttribute _headersAttribute = new HeadersAttribute();
        private readonly RowSpanAttribute _rowSpanAttribue = new RowSpanAttribute();
        private readonly ScopeAttribute _scopeAttribute = new ScopeAttribute();
        private readonly SortedAttribute _sortedAttribute = new SortedAttribute();



        /// <summary>
        /// Abbreviated form of the cell's content.
        /// </summary>
        public AbbreviatedAttribute Abbr { get { return _abbrAttribute; } }


        /// <summary>
        /// This attribute specifies the number of columns spanned by the current cell.
        /// </summary>
        public ColSpanAttribute ColSpan { get { return _colSpanAttribute; } }


        /// <summary>
        /// Specifies one or more header cells a cell is related to
        /// </summary>
        public HeadersAttribute Headers { get { return _headersAttribute; }}

        /// <summary>
        /// This attribute specifies the number of rows spanned by the current cell.
        /// </summary>
        public RowSpanAttribute RowSpan { get { return _rowSpanAttribue; } }


        /// <summary>
        /// Defines the sort direction of a column
        /// </summary>
        public SortedAttribute Sorted { get { return _sortedAttribute; }}


        /// <summary>
        ///     This attribute specifies the set of data cells for which the current header cell provides header information. 
        /// When specified, this attribute must have one of the following values:
        /// 
        /// * row: The current cell provides header information for the rest of the row that contains it.
        /// * col: The current cell provides header information for the rest of the column that contains it.
        /// * rowgroup: The header cell provides header information for the rest of the row group that contains it.
        /// * colgroup: The header cell provides header information for the rest of the column group that contains it.
        /// </summary>
        public ScopeAttribute Scope { get { return _scopeAttribute; } }


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
