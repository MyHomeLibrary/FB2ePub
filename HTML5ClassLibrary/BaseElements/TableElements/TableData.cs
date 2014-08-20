using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The td element defines a data cell in a table (i.e. cells that are not header cells).
    /// </summary>
    [HTMLItemAttribute(ElementName = "td", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class TableData : HTMLItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AbbreviatedAttribute _abbreviatedAttribute = new AbbreviatedAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignAttribute _alignAttribute = new AlignAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AxisAttribute _axisAttribute = new AxisAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BackgroundColorAttribute _backgroundColorAttribute = new BackgroundColorAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharAttribute _charAttribute = new CharAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharOffAttribute _charOffAttribute = new CharOffAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ColSpanAttribute _colSpanAttribute = new ColSpanAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HeadersAttribute _headersAttribute = new HeadersAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NoWrapAttribute _noWrapAttribute = new NoWrapAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly RowSpanAttribute _rowSpanAttribue = new RowSpanAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ScopeAttribute _scopeAttribute = new ScopeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VAlignAttribute _vAlignAttribute = new VAlignAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();



        /// <summary>
        /// Specifies an abbreviated version of the content in a cell
        /// Not supported in HTML5.
        /// </summary>
        public AbbreviatedAttribute Abbr { get { return _abbreviatedAttribute; }}


        /// <summary>
        /// Aligns the content in a cell
        /// Not supported in HTML5.
        /// </summary>
        public AlignAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        /// Categorizes cells
        /// Not supported in HTML5.
        /// </summary>
        public AxisAttribute Axis { get { return _axisAttribute; }}


        /// <summary>
        ///  Specifies the background color of a cell
        /// Not supported in HTML5.
        /// </summary>
        public BackgroundColorAttribute BgColor { get { return _backgroundColorAttribute; }}


        /// <summary>
        /// Aligns the content in a cell to a character
        /// Not supported in HTML5.
        /// </summary>
        public CharAttribute Char { get { return _charAttribute; } }


        /// <summary>
        /// Sets the number of characters the content will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public CharOffAttribute CharOff { get { return _charOffAttribute; }}

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


        /// <summary>
        ///  Sets the height of a cell
        /// Not supported in HTML5.
        /// </summary>
        public HeightAttribute Height { get { return _heightAttribute; }}


        /// <summary>
        ///  Specifies that the content inside a cell should not wrap
        /// Not supported in HTML5.
        /// </summary>
        public NoWrapAttribute NoWrap { get { return _noWrapAttribute; }}


        /// <summary>
        /// Defines a way to associate header cells and data cells in a table
        /// Not supported in HTML5.
        /// </summary>
        public ScopeAttribute Scope { get { return _scopeAttribute; }}


        /// <summary>
        /// Vertical aligns the content in a cell
        /// Not supported in HTML5.
        /// </summary>
        public VAlignAttribute VAlign { get { return _vAlignAttribute; }}


        /// <summary>
        ///  Specifies the width of a cell
        /// Not supported in HTML5.
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; }}




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
