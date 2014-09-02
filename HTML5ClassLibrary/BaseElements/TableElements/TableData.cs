using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
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
        #region Attribute_Values_Enums
        /// <summary>
        /// align attribute possible values
        /// </summary>
        public enum AlignAttributeOptions
        {
            [Description("center")]
            Center,

            [Description("char")]
            Char,

            [Description("justify")]
            Justify,

            [Description("left")]
            Left,

            [Description("right")]
            Right,
        }

        /// <summary>
        /// "valign" attribute possible values
        /// </summary>
        public enum VAlignAttributeOptions
        {
            [Description("baseline")]
            Baseline,

            [Description("bottom")]
            Bottom,

            [Description("middle")]
            Middle,

            [Description("top")]
            Top,
        }

        /// <summary>
        /// "scope" attribute possible values
        /// </summary>
        public enum ScopeAttributeOptions
        {
            [Description("col")]
            Col,

            [Description("colgroup")]
            ColGroup,

            [Description("row")]
            Row,

            [Description("rowgroup")]
            RowGroup,
        }

        #endregion

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _abbreviatedAttribute = new SimpleSingleTypeAttribute<Text>("abbr");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("align",typeof(AlignAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _axisAttribute = new SimpleSingleTypeAttribute<Text>("axis");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Color> _backgroundColorAttribute = new SimpleSingleTypeAttribute<Color>("bgcolor");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Character> _charAttribute = new SimpleSingleTypeAttribute<Character>("char");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _charOffAttribute = new SimpleSingleTypeAttribute<Length>("charoff");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _colSpanAttribute = new SimpleSingleTypeAttribute<Number>("colspan");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<NameTokens> _headersAttribute = new SimpleSingleTypeAttribute<NameTokens>("headers");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _heightAttribute = new SimpleSingleTypeAttribute<Length>("height");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _noWrapAttribute = new FlagTypeAttribute("nowrap");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _rowSpanAttribue = new SimpleSingleTypeAttribute<Number>("rowspan");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _scopeAttribute = new ValuesSelectionTypeAttribute<Text>("scope",typeof(ScopeAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _vAlignAttribute = new ValuesSelectionTypeAttribute<Text>("valign",typeof(VAlignAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>("width");


        public TableData(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies an abbreviated version of the content in a cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Abbr { get { return _abbreviatedAttribute; } }


        /// <summary>
        /// Aligns the content in a cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }


        /// <summary>
        /// Categorizes cells
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Axis { get { return _axisAttribute; } }


        /// <summary>
        ///  Specifies the background color of a cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess BgColor { get { return _backgroundColorAttribute; } }


        /// <summary>
        /// Aligns the content in a cell to a character
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Char { get { return _charAttribute; } }


        /// <summary>
        /// Sets the number of characters the content will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess CharOff { get { return _charOffAttribute; } }

        /// <summary>
        /// This attribute specifies the number of columns spanned by the current cell.
        /// </summary>
        public IAttributeDataAccess ColSpan { get { return _colSpanAttribute; } }


        /// <summary>
        /// This attribute specifies the number of rows spanned by the current cell.
        /// </summary>
        public IAttributeDataAccess RowSpan { get { return _rowSpanAttribue; } }


        /// <summary>
        /// Specifies one or more header cells a cell is related to
        /// </summary>
        public IAttributeDataAccess Headers { get { return _headersAttribute; } }


        /// <summary>
        ///  Sets the height of a cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Height { get { return _heightAttribute; } }


        /// <summary>
        ///  Specifies that the content inside a cell should not wrap
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess NoWrap { get { return _noWrapAttribute; } }


        /// <summary>
        /// Defines a way to associate header cells and data cells in a table
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Scope { get { return _scopeAttribute; } }


        /// <summary>
        /// Vertical aligns the content in a cell
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess VAlign { get { return _vAlignAttribute; } }


        /// <summary>
        ///  Specifies the width of a cell
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
