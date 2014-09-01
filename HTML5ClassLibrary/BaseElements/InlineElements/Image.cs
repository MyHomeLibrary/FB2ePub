using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The img element is used to define an image.
    /// </summary>
    [HTMLItemAttribute(ElementName = "img", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Image : HTMLItem, IInlineItem
    {
        #region Attribute_Values_Enums

        /// <summary>
        /// "align" attribute possible options
        /// </summary>
        public enum AlignAttributeOptions
        {
            [Description("baseline")]
            Baseline,

            [Description("bottom")]
            Bottom,

            [Description("left")]
            Left,

            [Description("middle")]
            Middle,

            [Description("right")]
            Right,

            [Description("top")]
            Top
        }


        /// <summary>
        /// "crossorigin" attribute possible values
        /// </summary>
        public enum CrossOriginAttributeOptions
        {
            [Description("anonymous")]
            Anonymous,

            [Description("use-credentials")]
            UseCredentials,
        }

        #endregion 



        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("align",typeof(AlignAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _altAttribute = new SimpleSingleTypeAttribute<Text>("alt");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Pixels> _borderAttribute = new SimpleSingleTypeAttribute<Pixels>("border");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _crossOriginAttribute = new ValuesSelectionTypeAttribute<Text>("crossorigin",typeof(CrossOriginAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _heightAttribute = new SimpleSingleTypeAttribute<Length>("height");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _hSpaceAttribute = new SimpleSingleTypeAttribute<Length>("hspace");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _ismapAttribute = new FlagTypeAttribute("ismap",true);

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _longDescriptionAttribute = new SimpleSingleTypeAttribute<URI>("longdesc");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _srcAttribute = new SimpleSingleTypeAttribute<URI>("src");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<IdReference> _useMapAttribute = new SimpleSingleTypeAttribute<IdReference>("usemap");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _vSpaceAttribute = new SimpleSingleTypeAttribute<Length>("vspace");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>("width");


        #region public_attributes


        /// <summary>
        ///  Specifies the alignment of an image according to surrounding elements
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }

        /// <summary>
        /// Alternate text for the image. This attribute is required. 
        /// The value should be left blank for decorative images.
        /// </summary>
        public IAttributeDataAccess Alt
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
        public IAttributeDataAccess Border { get { return _borderAttribute; } }

        /// <summary>
        /// Image height.
        /// </summary>
        public IAttributeDataAccess Height
        {
            get { return _heightAttribute; }
        }


        /// <summary>
        ///  Specifies the whitespace on left and right side of an image
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess HSpace { get { return _hSpaceAttribute; } }


        /// <summary>
        /// Image width.
        /// </summary>
        public IAttributeDataAccess Width
        {
            get { return _widthAttribute; }
        }

        /// <summary>
        /// This required attribute specifies the location of the image source.
        /// </summary>
        public IAttributeDataAccess Source
        {
            get { return _srcAttribute; }
        }

        /// <summary>
        /// If present, this attribute specifies that a server-side image map should be used. 
        /// Possible value is "ismap".
        /// </summary>
        public IAttributeDataAccess ISMap
        {
            get { return _ismapAttribute; }
        }


        /// <summary>
        ///  Specifies the URL to a document that contains a long description of an image
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess LongDescription { get { return _longDescriptionAttribute; } }

        /// <summary>
        /// This attribute associates the image to a client-side image map defined by a map element. 
        /// The value of this attribute must match the id attribute of the map element.
        /// </summary>
        public IAttributeDataAccess UseMap
        {
            get { return _useMapAttribute; }
        }


        /// <summary>
        ///  Specifies the whitespace on top and bottom of an image
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess VSpace { get { return _vSpaceAttribute; } }

        /// <summary>
        /// Allow images from third-party sites that allow cross-origin access to be used with canvas
        /// </summary>
        public IAttributeDataAccess Crossorigin { get { return _crossOriginAttribute; } }

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
