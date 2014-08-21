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
        [AttributeTypeAttributeMember(Name = "abbr", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _abbrAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignTypeAttribute _alignAttribute = new AlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "axis", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _axisAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(Name = "bgcolor", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ColorTypeAttribute _backgroundColorAttribute = new ColorTypeAttribute();

        [AttributeTypeAttributeMember(Name = "char", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharAttribute _charAttribute = new CharAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharOffAttribute _charOffAttribute = new CharOffAttribute();

        [AttributeTypeAttributeMember(Name = "colspan", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NumberAttribute _colSpanAttribute = new NumberAttribute();

        [AttributeTypeAttributeMember(Name = "headers", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameTokensAttribute _headersAttribute = new NameTokensAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _heightAttribute = new LengthAttribute();

        [AttributeTypeAttributeMember(Name = "nowrap", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _noWrapAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "rowspan", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NumberAttribute _rowSpanAttribue = new NumberAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ScopeAttribute _scopeAttribute = new ScopeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SortedAttribute _sortedAttribute = new SortedAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VAlignAttribute _vAlignAttribute = new VAlignAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _widthAttribute = new LengthAttribute();



        /// <summary>
        /// Abbreviated form of the cell's content.
        /// </summary>
        public TextValueAttribute Abbr { get { return _abbrAttribute; } }


        /// <summary>
        ///  Aligns the content in a header cell
        /// Not supported in HTML5.
        /// </summary>
        public AlignTypeAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        ///  Categorizes header cells
        /// Not supported in HTML5.
        /// </summary>
        public TextValueAttribute Axis { get { return _axisAttribute; }}


        /// <summary>
        ///  Specifies the background color of a header cell
        /// Not supported in HTML5.
        /// </summary>
        public ColorTypeAttribute BgColor { get { return _backgroundColorAttribute; }}


        /// <summary>
        ///  Aligns the content in a header cell to a character
        /// Not supported in HTML5.
        /// </summary>
        public CharAttribute Char { get { return _charAttribute; }}


        /// <summary>
        ///  Sets the number of characters the content will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public CharOffAttribute CharOff { get { return _charOffAttribute; }}

        /// <summary>
        /// This attribute specifies the number of columns spanned by the current cell.
        /// </summary>
        public NumberAttribute ColSpan { get { return _colSpanAttribute; } }


        /// <summary>
        /// Specifies one or more header cells a cell is related to
        /// </summary>
        public NameTokensAttribute Headers { get { return _headersAttribute; }}


        /// <summary>
        ///  Sets the height of a header cell
        /// Not supported in HTML5.
        /// </summary>
        public LengthAttribute Height { get { return _heightAttribute; }}


        /// <summary>
        ///  Specifies that the content inside a header cell should not wrap
        /// Not supported in HTML5.
        /// </summary>
        public FlagTypeAttribute NoWrap { get { return _noWrapAttribute; }}

        /// <summary>
        /// This attribute specifies the number of rows spanned by the current cell.
        /// </summary>
        public NumberAttribute RowSpan { get { return _rowSpanAttribue; } }


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


        /// <summary>
        ///  Vertical aligns the content in a header cell
        /// Not supported in HTML5.
        /// </summary>
        public VAlignAttribute VAlign { get { return _vAlignAttribute; }}


        /// <summary>
        ///  Specifies the width of a header cell
        /// Not supported in HTML5.
        /// </summary>
        public LengthAttribute Width { get { return _widthAttribute; }}



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
