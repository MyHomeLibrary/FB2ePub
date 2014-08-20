using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    [HTMLItemAttribute(ElementName = "basefont", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]    
    public class BaseFont : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly ColorAttribute _colorAttribute = new ColorAttribute();

        [AttributeTypeAttributeMember(Name = "face", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _faceAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SizeAttribute _sizeAttribute = new SizeAttribute();


        /// <summary>
        /// Specifies the default color for text in a document
        /// </summary>
        public ColorAttribute Color { get { return _colorAttribute; }}

        /// <summary>
        /// Specifies the default font for text in a document
        /// </summary>
        public TextValueAttribute Face { get { return _faceAttribute; }}


        /// <summary>
        /// Specifies the default size of text in a document
        /// </summary>
        public SizeAttribute Size { get { return _sizeAttribute; }}

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
