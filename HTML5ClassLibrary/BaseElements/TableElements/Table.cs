using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
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
        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("center;justify;right;left;char");

        [AttributeTypeAttributeMember(Name = "bgcolor", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ColorTypeAttribute _backgroundColorAttribute = new ColorTypeAttribute();

        [AttributeTypeAttributeMember(Name = "border", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly PixelsTypeAttribute _borderAttribute = new PixelsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "cellpadding", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _cellPaddingAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "cellspacing", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _cellSpacingAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "frame", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FrameTypeAttribute _frameAttribute = new FrameTypeAttribute();

        [AttributeTypeAttributeMember(Name = "rules", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly RulesTypeAttribute _rulesAttribute = new RulesTypeAttribute();

        [AttributeTypeAttributeMember(Name = "sortable", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _sortableAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "summary", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _summaryAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _widthAttribute = new LengthTypeAttribute();




        /// <summary>
        ///  Specifies the alignment of a table according to surrounding text
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }


        /// <summary>
        ///  Specifies the background color for a table
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess BgColor { get { return _backgroundColorAttribute; } }


        /// <summary>
        /// Specifies whether the table cells should have borders or not
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Border { get { return _borderAttribute; } }


        /// <summary>
        /// Specifies the space between the cell wall and the cell content
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess CellPadding { get { return _cellPaddingAttribute; } }


        /// <summary>
        /// Specifies the space between cells
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess CellSpacing { get { return _cellSpacingAttribute; } }


        /// <summary>
        /// Specifies which parts of the outside borders that should be visible
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Frame { get { return _frameAttribute; } }


        /// <summary>
        /// Specifies which parts of the inside borders that should be visible
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Rules { get { return _rulesAttribute; } }


        /// <summary>
        /// Specifies that the table should be sortable
        /// </summary>
        public IAttributeDataAccess Sortable { get { return _sortableAttribute; } }


        /// <summary>
        /// Specifies a summary of the content of a table
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Summary { get { return _summaryAttribute; } }


        /// <summary>
        /// Specifies the width of a table
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; } }



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
