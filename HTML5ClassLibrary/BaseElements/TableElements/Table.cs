using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The table element is used to define a table. 
    /// A table is a construct where data is organized into rows and columns of cells.
    /// </summary>
    [HTMLItemAttribute(ElementName = "table", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Table : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignAttribute _alignAttribute = new AlignAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BackgroundColorAttribute _backgroundColorAttribute = new BackgroundColorAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BorderAttribute _borderAttribute = new BorderAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CellPaddingAttribute _cellPaddingAttribute = new CellPaddingAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CellSpacingAttribute _cellSpacingAttribute = new CellSpacingAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FrameAttribute _frameAttribute = new FrameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly RulesAttribute _rulesAttribute = new RulesAttribute();

        [AttributeTypeAttributeMember(Name = "sortable", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _sortableAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SummaryAttribute _summaryAttribute = new SummaryAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();




        /// <summary>
        ///  Specifies the alignment of a table according to surrounding text
        /// Not supported in HTML5.
        /// </summary>
        public AlignAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        ///  Specifies the background color for a table
        /// Not supported in HTML5.
        /// </summary>
        public BackgroundColorAttribute BgColor { get { return _backgroundColorAttribute; }}


        /// <summary>
        /// Specifies whether the table cells should have borders or not
        /// Not supported in HTML5.
        /// </summary>
        public BorderAttribute Border { get { return _borderAttribute; }}


        /// <summary>
        /// Specifies the space between the cell wall and the cell content
        /// Not supported in HTML5.
        /// </summary>
        public CellPaddingAttribute CellPadding { get { return _cellPaddingAttribute; }}


        /// <summary>
        /// Specifies the space between cells
        /// Not supported in HTML5.
        /// </summary>
        public CellSpacingAttribute CellSpacing { get { return _cellSpacingAttribute; }}


        /// <summary>
        /// Specifies which parts of the outside borders that should be visible
        /// Not supported in HTML5.
        /// </summary>
        public FrameAttribute Frame { get { return _frameAttribute; }}


        /// <summary>
        /// Specifies which parts of the inside borders that should be visible
        /// Not supported in HTML5.
        /// </summary>
        public RulesAttribute Rules { get { return _rulesAttribute; }}


        /// <summary>
        /// Specifies that the table should be sortable
        /// </summary>
        public FlagAttribute Sortable { get { return _sortableAttribute; }}


        /// <summary>
        /// Specifies a summary of the content of a table
        /// Not supported in HTML5.
        /// </summary>
        public SummaryAttribute Summary { get { return _summaryAttribute; }}


        /// <summary>
        /// Specifies the width of a table
        /// Not supported in HTML5.
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; }}



        protected override bool IsValidSubType(IHTMLItem item)
        {
            // may appear only once and only as first element
            if (item is TableCaption)
            {
                if (Subitems.Count > 0)
                {
                    return false;
                }
                IHTMLItem seekItem = Subitems.Find(x => x is TableCaption);
                if (seekItem != null)
                {
                    return false;
                }
                return item.IsValid();
            }
            if (item is ColElement)
            {
                return item.IsValid();
            }
            if (item is ColGroup)
            {
                return item.IsValid();
            }
            if (item is TableBody)
            {
                IHTMLItem seekItem = Subitems.Find(x => x is TableBody);
                if (seekItem != null)
                {
                    return false;
                }
                seekItem = Subitems.Find(x => x is TableRow);
                if (seekItem != null)
                {
                    return false;
                }
                return item.IsValid();
            }
            if (item is TableRow)
            {
                IHTMLItem seekItem = Subitems.Find(x => x is TableBody);
                if (seekItem != null)
                {
                    return false;
                }
                seekItem = Subitems.Find(x => x is TableHead);
                if (seekItem != null)
                {
                    return false;
                }
                seekItem = Subitems.Find(x => x is TableFooter);
                if (seekItem != null)
                {
                    return false;
                }
                return item.IsValid();
            }
            return false;
        }



        public override bool IsValid()
        {
            // TODO: perform full validation based on:
            //
            // The following element may appear only as the first one inside table :
            // * caption may appear at most once
            // Either one or the other or neither of the following two elements may then appear:
            //
            // * col may appear any number of times or not at all
            // * colgroup may appear any number of times or not at all
            //
            // Finally, one or more of the following elements must then appear in the order listed:
            // * thead may appear at most once, and only if tbody appears
            // * tfoot may appear at most once, and only if tbody appears
            // * tbody must appear at least once if, and only if, tr does not appear
            // * tr must appear at least once if, and only if, tbody does not appear
            return true;
        }
    }
}
