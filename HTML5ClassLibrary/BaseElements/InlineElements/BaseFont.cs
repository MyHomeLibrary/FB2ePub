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
        private readonly TextValueAttribute _faceAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(Name = "size", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly NumberAttribute _sizeAttribute = new NumberAttribute();


        /// <summary>
        /// Specifies the default color for text in a document
        /// </summary>
        public ColorTypeAttribute Color { get { return _colorAttribute; }}

        /// <summary>
        /// Specifies the default font for text in a document
        /// </summary>
        public TextValueAttribute Face { get { return _faceAttribute; }}


        /// <summary>
        /// Specifies the default size of text in a document
        /// </summary>
        public NumberAttribute Size { get { return _sizeAttribute; }}

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
