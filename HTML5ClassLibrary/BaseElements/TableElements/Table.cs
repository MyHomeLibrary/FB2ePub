using System.ComponentModel;
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

        #region Attribute_Values_Enums

        /// <summary>
        /// "align" attribute possible values
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
        /// "frame" attribute possible values
        /// </summary>
        public enum FrameAttributeOptions
        {
            [Description("above")]
            Above,

            [Description("below")]
            Below,

            [Description("border")]
            Border,

            [Description("box")]
            Box,

            [Description("hsides")]
            HSides,

            [Description("lhs")]
            Lhs,

            [Description("rhs")]
            Rhs,

            [Description("void")]
            Void,

            [Description("vsides")]
            VSides,
        }


        /// <summary>
        /// "rules" attribute possible values
        /// </summary>
        public enum RulesAttributeOptions
        {
            [Description("all")]
            All,

            [Description("cols")]
            Cols,

            [Description("groups")]
            Groups,

            [Description("none")]
            None,

            [Description("rows")]
            Rows,
        }

        #endregion


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("align",typeof(AlignAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Color> _backgroundColorAttribute = new SimpleSingleTypeAttribute<Color>("bgcolor");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Pixels> _borderAttribute = new SimpleSingleTypeAttribute<Pixels>("border");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _cellPaddingAttribute = new SimpleSingleTypeAttribute<Length>("cellpadding");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _cellSpacingAttribute = new SimpleSingleTypeAttribute<Length>("cellspacing");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _frameAttribute = new ValuesSelectionTypeAttribute<Text>("frame",typeof(FrameAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _rulesAttribute = new ValuesSelectionTypeAttribute<Text>("rules",typeof(RulesAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _sortableAttribute = new FlagTypeAttribute("sortable");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _summaryAttribute = new SimpleSingleTypeAttribute<Text>("summary");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>("width");




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
