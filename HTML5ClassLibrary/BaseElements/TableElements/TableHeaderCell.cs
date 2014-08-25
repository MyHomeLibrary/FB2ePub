using XHTMLClassLibrary.AttributeDataTypes;
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
        private readonly TextValueTypeAttribute _abbrAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("center;justify;right;left;char");

        [AttributeTypeAttributeMember(Name = "axis", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _axisAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "bgcolor", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ColorTypeAttribute _backgroundColorAttribute = new ColorTypeAttribute();

        [AttributeTypeAttributeMember(Name = "char", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharAttribute _charAttribute = new CharAttribute();

        [AttributeTypeAttributeMember(Name = "charoff", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _charOffAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "colspan", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NumberTypeAttribute _colSpanAttribute = new NumberTypeAttribute();

        [AttributeTypeAttributeMember(Name = "headers", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameTokensTypeAttribute _headersAttribute = new NameTokensTypeAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _heightAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "nowrap", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _noWrapAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "rowspan", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NumberTypeAttribute _rowSpanAttribue = new NumberTypeAttribute();

        [AttributeTypeAttributeMember(Name = "scope", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ScopeTypeAttribute _scopeAttribute = new ScopeTypeAttribute();

        [AttributeTypeAttributeMember(Name = "sorted", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SortedTypeAttribute _sortedAttribute = new SortedTypeAttribute();

        [AttributeTypeAttributeMember(Name = "valign", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VAlignTypeAttribute _vAlignAttribute = new VAlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _widthAttribute = new LengthTypeAttribute();



        /// <summary>
        /// Abbreviated form of the cell's content.
        /// </summary>
        public IAttributeDataAccess Abbr { get { return _abbrAttribute; } }


        /// <summary>
        ///  Aligns the content in a header cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }


        /// <summary>
        ///  Categorizes header cells
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Axis { get { return _axisAttribute; } }


        /// <summary>
        ///  Specifies the background color of a header cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess BgColor { get { return _backgroundColorAttribute; } }


        /// <summary>
        ///  Aligns the content in a header cell to a character
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Char { get { return _charAttribute; } }


        /// <summary>
        ///  Sets the number of characters the content will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess CharOff { get { return _charOffAttribute; } }

        /// <summary>
        /// This attribute specifies the number of columns spanned by the current cell.
        /// </summary>
        public IAttributeDataAccess ColSpan { get { return _colSpanAttribute; } }


        /// <summary>
        /// Specifies one or more header cells a cell is related to
        /// </summary>
        public IAttributeDataAccess Headers { get { return _headersAttribute; } }


        /// <summary>
        ///  Sets the height of a header cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Height { get { return _heightAttribute; } }


        /// <summary>
        ///  Specifies that the content inside a header cell should not wrap
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess NoWrap { get { return _noWrapAttribute; } }

        /// <summary>
        /// This attribute specifies the number of rows spanned by the current cell.
        /// </summary>
        public IAttributeDataAccess RowSpan { get { return _rowSpanAttribue; } }


        /// <summary>
        /// Defines the sort direction of a column
        /// </summary>
        public IAttributeDataAccess Sorted { get { return _sortedAttribute; } }


        /// <summary>
        ///     This attribute specifies the set of data cells for which the current header cell provides header information. 
        /// When specified, this attribute must have one of the following values:
        /// 
        /// * row: The current cell provides header information for the rest of the row that contains it.
        /// * col: The current cell provides header information for the rest of the column that contains it.
        /// * rowgroup: The header cell provides header information for the rest of the row group that contains it.
        /// * colgroup: The header cell provides header information for the rest of the column group that contains it.
        /// </summary>
        public IAttributeDataAccess Scope { get { return _scopeAttribute; } }


        /// <summary>
        ///  Vertical aligns the content in a header cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess VAlign { get { return _vAlignAttribute; } }


        /// <summary>
        ///  Specifies the width of a header cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; } }



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
