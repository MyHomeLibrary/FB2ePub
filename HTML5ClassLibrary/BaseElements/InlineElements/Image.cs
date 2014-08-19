using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The img element is used to define an image.
    /// </summary>
    [HTMLItemAttribute(ElementName = "img", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Image : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignAttribute _alignAttribute = new BiDirectionalAlignAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AltAttribute _altAttribute = new AltAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BorderAttribute _borderAttribute = new BorderAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly CrossOriginAttribute _crossOriginAttribute = new CrossOriginAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HSpaceAttribute _hSpaceAttribute = new HSpaceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ISMapAttribute _ismapAttribute = new ISMapAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LongDescriptionAttribute _longDescriptionAttribute = new LongDescriptionAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SourceAttribute _srcAttribute = new SourceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly UseMapAttribute _useMapAttribute = new UseMapAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VSpaceAttribute _vSpaceAttribute = new VSpaceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();


        #region public_attributes


        /// <summary>
        ///  Specifies the alignment of an image according to surrounding elements
        /// Not supported in HTML5.
        /// </summary>
        public BiDirectionalAlignAttribute Align { get { return _alignAttribute; }}

        /// <summary>
        /// Alternate text for the image. This attribute is required. 
        /// The value should be left blank for decorative images.
        /// </summary>
        public AltAttribute Alt
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
        public BorderAttribute Border { get { return _borderAttribute; }}

        /// <summary>
        /// Image height.
        /// </summary>
        public HeightAttribute Height
        {
            get { return _heightAttribute; }
        }


        /// <summary>
        ///  Specifies the whitespace on left and right side of an image
        /// Not supported in HTML5.
        /// </summary>
        public HSpaceAttribute HSpace { get { return _hSpaceAttribute; }}


        /// <summary>
        /// Image width.
        /// </summary>
        public  WidthAttribute Width
        {
            get { return _widthAttribute; }
        }

        /// <summary>
        /// This required attribute specifies the location of the image source.
        /// </summary>
        public  SourceAttribute Source
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
        public LongDescriptionAttribute LongDescription { get { return _longDescriptionAttribute; }}

        /// <summary>
        /// This attribute associates the image to a client-side image map defined by a map element. 
        /// The value of this attribute must match the id attribute of the map element.
        /// </summary>
        public UseMapAttribute UseMap
        {
            get { return _useMapAttribute; }
        }


        /// <summary>
        ///  Specifies the whitespace on top and bottom of an image
        /// Not supported in HTML5.
        /// </summary>
        public VSpaceAttribute VSpace { get { return _vSpaceAttribute; }}

        /// <summary>
        /// Allow images from third-party sites that allow cross-origin access to be used with canvas
        /// </summary>
        public CrossOriginAttribute Crossorigin { get { return _crossOriginAttribute; }}

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
