using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    [HTMLItemAttribute(ElementName = "basefont", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]    
    public class BaseFont : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "color", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly ColorTypeAttribute _colorAttribute = new ColorTypeAttribute();

        [AttributeTypeAttributeMember(Name = "face", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _faceAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "size", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly NumberTypeAttribute _sizeAttribute = new NumberTypeAttribute();


        /// <summary>
        /// Specifies the default color for text in a document
        /// </summary>
        public ColorTypeAttribute Color { get { return _colorAttribute; }}

        /// <summary>
        /// Specifies the default font for text in a document
        /// </summary>
        public TextValueTypeAttribute Face { get { return _faceAttribute; }}


        /// <summary>
        /// Specifies the default size of text in a document
        /// </summary>
        public NumberTypeAttribute Size { get { return _sizeAttribute; }}

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
