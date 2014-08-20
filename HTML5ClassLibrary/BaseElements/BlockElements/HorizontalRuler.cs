using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The hr element is used to separate sections of content. 
    /// Though the name of the hr element is "horizontal rule", most visual Web browsers render hr as a horizontal line.
    /// </summary>
    [HTMLItemAttribute(ElementName = "hr", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class HorizontalRuler : HTMLItem, IBlockElement 
    {
        [AttributeTypeAttributeMember(SupportedStandards =  HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
        private readonly  AlignAttribute _alignAttribute = new AlignAttribute();


        [AttributeTypeAttributeMember(Name = "noshade", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
        private readonly FlagAttribute _noShadeAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
        private readonly SizeAttribute _sizeAttribute = new SizeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
        private readonly  WidthAttribute _widthAttribute = new WidthAttribute();



        /// <summary>
        ///  Specifies that a "hr" element should render in one solid color (noshaded), instead of a shaded color
        /// Not supported in HTML5.
        /// </summary>
        public FlagAttribute NoShade { get { return _noShadeAttribute; }}


        /// <summary>
        ///  Specifies the alignment of a "hr" element
        /// Not supported in HTML5.
        /// </summary>
        public AlignAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        ///  Specifies the height of a "hr" element
        /// Not supported in HTML5.
        /// </summary>
        public SizeAttribute Size { get { return _sizeAttribute; }}


        /// <summary>
        ///  Specifies the width of a "hr" element
        /// Not supported in HTML5.
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; }}

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
