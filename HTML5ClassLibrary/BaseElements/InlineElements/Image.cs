using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The img element is used to define an image.
    /// </summary>
    [HTMLItemAttribute(ElementName = "img", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Image : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignTypeAttribute _alignAttribute = new BiDirectionalAlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "alt", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _altAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(Name = "border", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly PixelsTypeAttribute _borderAttribute = new PixelsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "crossorigin", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly CrossOriginTypeAttribute _crossOriginAttribute = new CrossOriginTypeAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _heightAttribute = new LengthAttribute();

        [AttributeTypeAttributeMember(Name = "hspace", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _hSpaceAttribute = new LengthAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ISMapAttribute _ismapAttribute = new ISMapAttribute();

        [AttributeTypeAttributeMember(Name = "longdesc", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _longDescriptionAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _srcAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "usemap", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly IdReferenceAttribute _useMapAttribute = new IdReferenceAttribute();

        [AttributeTypeAttributeMember(Name = "vspace", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _vSpaceAttribute = new LengthAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _widthAttribute = new LengthAttribute();


        #region public_attributes


        /// <summary>
        ///  Specifies the alignment of an image according to surrounding elements
        /// Not supported in HTML5.
        /// </summary>
        public BiDirectionalAlignTypeAttribute Align { get { return _alignAttribute; }}

        /// <summary>
        /// Alternate text for the image. This attribute is required. 
        /// The value should be left blank for decorative images.
        /// </summary>
        public TextValueAttribute Alt
        {
            get
            {
                return _altAttribute;
            }
        }

        /// <summary>
        ///  Specifies the width of the border around an image
        /// Not supported in HTML5.
        /// </summary>
        public PixelsTypeAttribute Border { get { return _borderAttribute; }}

        /// <summary>
        /// Image height.
        /// </summary>
        public LengthAttribute Height
        {
            get { return _heightAttribute; }
        }


        /// <summary>
        ///  Specifies the whitespace on left and right side of an image
        /// Not supported in HTML5.
        /// </summary>
        public LengthAttribute HSpace { get { return _hSpaceAttribute; }}


        /// <summary>
        /// Image width.
        /// </summary>
        public  LengthAttribute Width
        {
            get { return _widthAttribute; }
        }

        /// <summary>
        /// This required attribute specifies the location of the image source.
        /// </summary>
        public  URITypeAttribute Source
        {
            get { return _srcAttribute; }
        }

        /// <summary>
        /// If present, this attribute specifies that a server-side image map should be used. 
        /// Possible value is "ismap".
        /// </summary>
        public ISMapAttribute ISMap
        {
            get { return _ismapAttribute; }
        }


        /// <summary>
        ///  Specifies the URL to a document that contains a long description of an image
        /// Not supported in HTML5.
        /// </summary>
        public URITypeAttribute LongDescription { get { return _longDescriptionAttribute; }}

        /// <summary>
        /// This attribute associates the image to a client-side image map defined by a map element. 
        /// The value of this attribute must match the id attribute of the map element.
        /// </summary>
        public IdReferenceAttribute UseMap
        {
            get { return _useMapAttribute; }
        }


        /// <summary>
        ///  Specifies the whitespace on top and bottom of an image
        /// Not supported in HTML5.
        /// </summary>
        public LengthAttribute VSpace { get { return _vSpaceAttribute; }}

        /// <summary>
        /// Allow images from third-party sites that allow cross-origin access to be used with canvas
        /// </summary>
        public CrossOriginTypeAttribute Crossorigin { get { return _crossOriginAttribute; }}

        #endregion




        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return (_srcAttribute.HasValue());
        }


        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
