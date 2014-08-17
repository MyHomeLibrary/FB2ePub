using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "font" tag specifies the font face, font size, and color of text.
    /// </summary>
    [HTMLItemAttribute(ElementName = "font", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class Font : TextBasedElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private  readonly  ColorAttribute _colorAttribute = new ColorAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly  FaceAttribute _faceAttribute = new FaceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SizeAttribute _sizeAttribute = new SizeAttribute();


        /// <summary>
        /// Specifies the color of text
        /// </summary>
        public ColorAttribute Color { get { return _colorAttribute; }}


        /// <summary>
        /// Specifies the font of text
        /// </summary>
        public FaceAttribute Face { get { return _faceAttribute; }}

        /// <summary>
        /// Specifies the size of text
        /// </summary>
        public SizeAttribute Size { get { return _sizeAttribute; }}

    }
}
