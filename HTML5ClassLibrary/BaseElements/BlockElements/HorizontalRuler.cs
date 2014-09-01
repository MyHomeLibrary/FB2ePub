using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The hr element is used to separate sections of content. 
    /// Though the name of the hr element is "horizontal rule", most visual Web browsers render hr as a horizontal line.
    /// </summary>
    [HTMLItemAttribute(ElementName = "hr", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class HorizontalRuler : HTMLItem, IBlockElement
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

        #endregion 


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("align",typeof(AlignAttributeOptions));


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
        private readonly FlagTypeAttribute _noShadeAttribute = new FlagTypeAttribute("noshade");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
        private readonly SimpleSingleTypeAttribute<Number> _sizeAttribute = new SimpleSingleTypeAttribute<Number>("size");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>("width");



        /// <summary>
        ///  Specifies that a "hr" element should render in one solid color (noshaded), instead of a shaded color
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess NoShade { get { return _noShadeAttribute; } }


        /// <summary>
        ///  Specifies the alignment of a "hr" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }


        /// <summary>
        ///  Specifies the height of a "hr" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Size { get { return _sizeAttribute; } }


        /// <summary>
        ///  Specifies the width of a "hr" element
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; } }

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
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
