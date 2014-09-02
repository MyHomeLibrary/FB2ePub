using System.Collections.Generic;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    [HTMLItemAttribute(ElementName = "basefont", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]    
    public class BaseFont : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Color> _colorAttribute = new SimpleSingleTypeAttribute<Color>("color");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _faceAttribute = new SimpleSingleTypeAttribute<Text>("face");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _sizeAttribute = new SimpleSingleTypeAttribute<Number>("size");


        public BaseFont(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies the default color for text in a document
        /// </summary>
        public IAttributeDataAccess Color { get { return _colorAttribute; }}

        /// <summary>
        /// Specifies the default font for text in a document
        /// </summary>
        public IAttributeDataAccess Face { get { return _faceAttribute; }}


        /// <summary>
        /// Specifies the default size of text in a document
        /// </summary>
        public IAttributeDataAccess Size { get { return _sizeAttribute; }}



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
