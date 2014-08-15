using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;

namespace HTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The img element is used to define an image.
    /// </summary>
    [HTMLItemAttribute(ElementName = "img", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Image : HTMLItem, IInlineItem
    {
        public Image()
        {
            RegisterAttribute(_altAttribute);
            RegisterAttribute(_heightAttribute);
            RegisterAttribute(_srcAttribute);
            RegisterAttribute(_ismapAttribute);
            RegisterAttribute(_useMapAttribute);
            RegisterAttribute(_widthAttribute);
            RegisterAttribute(_crossOriginAttribute);
        }


        private readonly AltAttribute _altAttribute = new AltAttribute();
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();
        private readonly SourceAttribute _srcAttribute = new SourceAttribute();
        private readonly ISMapAttribute _ismapAttribute = new ISMapAttribute();
        private readonly UseMapAttribute _useMapAttribute = new UseMapAttribute();
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();
        private readonly CrossOriginAttribute _crossOriginAttribute = new CrossOriginAttribute();


        #region public_attributes


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
        /// Image height.
        /// </summary>
        public HeightAttribute Height
        {
            get { return _heightAttribute; }
        }

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
        /// This attribute associates the image to a client-side image map defined by a map element. 
        /// The value of this attribute must match the id attribute of the map element.
        /// </summary>
        public UseMapAttribute UseMap
        {
            get { return _useMapAttribute; }
        }

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
