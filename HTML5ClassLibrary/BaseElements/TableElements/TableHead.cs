using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The thead element can be used to group table rows that contain table header information. 
    /// This can be useful when printing long tables that span several printed pages, since the data in thead will be repeated on each page.
    /// </summary>
    [HTMLItemAttribute(ElementName = "thead", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class TableHead : HTMLItem
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

        #endregion

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("align",typeof(AlignAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Character> _charAttribute = new SimpleSingleTypeAttribute<Character>("char");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _charOffAttribute = new SimpleSingleTypeAttribute<Length>("charoff");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _vAlignAttribute = new ValuesSelectionTypeAttribute<Text>("valign",typeof(VAlignAttributeOptions));




        /// <summary>
        /// Aligns the content inside the "thead" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }


        /// <summary>
        /// Aligns the content inside the "thead" element to a character
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Char { get { return _charAttribute; } }


        /// <summary>
        /// Sets the number of characters the content inside the "thead" element will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess CharOff { get { return _charOffAttribute; } }


        /// <summary>
        /// Vertical aligns the content inside the "thead" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess VAlign { get { return _vAlignAttribute; } }


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is TableRow)
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
