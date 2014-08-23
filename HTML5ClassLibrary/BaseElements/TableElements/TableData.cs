using XHTMLClassLibrary.Attributes;
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
        [AttributeTypeAttributeMember(Name = "abbr", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _abbreviatedAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignTypeAttribute _alignAttribute = new AlignTypeAttribute();

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

        [AttributeTypeAttributeMember(Name = "scope", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ScopeTypeAttribute _scopeAttribute = new ScopeTypeAttribute();

        [AttributeTypeAttributeMember(Name = "valign", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VAlignTypeAttribute _vAlignAttribute = new VAlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _widthAttribute = new LengthTypeAttribute();



        /// <summary>
        /// Specifies an abbreviated version of the content in a cell
        /// Not supported in HTML5.
        /// </summary>
        public TextValueTypeAttribute Abbr { get { return _abbreviatedAttribute; } }


        /// <summary>
        /// Aligns the content in a cell
        /// Not supported in HTML5.
        /// </summary>
        public AlignTypeAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        /// Categorizes cells
        /// Not supported in HTML5.
        /// </summary>
        public TextValueTypeAttribute Axis { get { return _axisAttribute; }}


        /// <summary>
        ///  Specifies the background color of a cell
        /// Not supported in HTML5.
        /// </summary>
        public ColorTypeAttribute BgColor { get { return _backgroundColorAttribute; }}


        /// <summary>
        /// Aligns the content in a cell to a character
        /// Not supported in HTML5.
        /// </summary>
        public CharAttribute Char { get { return _charAttribute; } }


        /// <summary>
        /// Sets the number of characters the content will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public LengthTypeAttribute CharOff { get { return _charOffAttribute; }}

        /// <summary>
        /// This attribute specifies the number of columns spanned by the current cell.
        /// </summary>
        public NumberTypeAttribute ColSpan { get { return _colSpanAttribute; } }


        /// <summary>
        /// This attribute specifies the number of rows spanned by the current cell.
        /// </summary>
        public NumberTypeAttribute RowSpan { get { return _rowSpanAttribue; } }


        /// <summary>
        /// Specifies one or more header cells a cell is related to
        /// </summary>
        public NameTokensTypeAttribute Headers { get { return _headersAttribute; }}


        /// <summary>
        ///  Sets the height of a cell
        /// Not supported in HTML5.
        /// </summary>
        public LengthTypeAttribute Height { get { return _heightAttribute; }}


        /// <summary>
        ///  Specifies that the content inside a cell should not wrap
        /// Not supported in HTML5.
        /// </summary>
        public FlagTypeAttribute NoWrap { get { return _noWrapAttribute; }}


        /// <summary>
        /// Defines a way to associate header cells and data cells in a table
        /// Not supported in HTML5.
        /// </summary>
        public ScopeTypeAttribute Scope { get { return _scopeAttribute; }}


        /// <summary>
        /// Vertical aligns the content in a cell
        /// Not supported in HTML5.
        /// </summary>
        public VAlignTypeAttribute VAlign { get { return _vAlignAttribute; }}


        /// <summary>
        ///  Specifies the width of a cell
        /// Not supported in HTML5.
        /// </summary>
        public LengthTypeAttribute Width { get { return _widthAttribute; }}




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
