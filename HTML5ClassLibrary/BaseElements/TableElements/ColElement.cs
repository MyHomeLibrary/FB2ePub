using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// In XHTML, tables are physically constructed from rows, rather than columns. 
    /// Table rows contain table cells. 
    /// In visual Web browsers, when cells line up beneath each other, they are perceived as columns.
    /// </summary>
    [HTMLItemAttribute(ElementName = "col", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class ColElement : HTMLItem
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

        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(AlignAttributeOptions));

        [AttributeTypeAttributeMember(Name = "char", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Character> _charAttribute = new SimpleSingleTypeAttribute<Character>();

        [AttributeTypeAttributeMember(Name = "charoff", SupportedStandards =  HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _charOffAttribute = new SimpleSingleTypeAttribute<Length>();
        
        [AttributeTypeAttributeMember(Name = "span", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _spanAttribute = new SimpleSingleTypeAttribute<Number>();

        [AttributeTypeAttributeMember(Name = "valign", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _vAlignAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(VAlignAttributeOptions));

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>();


        /// <summary>
        /// A single col element can represent (or "span") multiple columns. 
        /// This attribute contains a number of columns "spanned" by the col element.
        /// </summary>
        public IAttributeDataAccess Span { get { return _spanAttribute; } }


        /// <summary>
        /// Not supported in HTML5.
        /// Specifies the alignment of the content related to a "col" element
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }


        /// <summary>
        /// Specifies the alignment of the content related to a "col" element to a character
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Char { get { return _charAttribute; } }


        /// <summary>
        /// Specifies the number of characters the content will be aligned from the character specified by the char attribute
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess CharOff { get { return _charOffAttribute; } }


        /// <summary>
        /// Specifies the vertical alignment of the content related to a "col" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess VAlign { get { return _vAlignAttribute; } }


        /// <summary>
        /// Specifies the width of a "col" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; } }




        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
