﻿using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.MapAreas
{
    /// <summary>
    /// The area element identifies geometric regions of a client-side image map, and provides a hyperlink for each region.
    /// </summary>
    [HTMLItemAttribute(ElementName = "area", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Area : HTMLItem
    {
        #region Attribute_Values_Enums

        /// <summary>
        /// "rel" attribute possible values
        /// </summary>
        public enum RelAttributeOptions
        {
            [Description("alternate")]
            Alternate,

            [Description("author")]
            Author,

            [Description("bookmark")]
            Bookmark,

            [Description("help")]
            Help,

            [Description("license")]
            License,

            [Description("nofollow")]
            NoFollow,

            [Description("next")]
            Next,

            [Description("norefferer")]
            NoRefferer,

            [Description("prefetch")]
            Prefetch,

            [Description("prev")]
            Prev,

            [Description("search")]
            Search,

            [Description("tag")]
            Tag,
        }


        /// <summary>
        /// "shape" attribute possible values
        /// </summary>
        public enum ShapeAttributeOptions
        {
            [Description("circle")]
            Circle,

            [Description("default")]
            Default,

            [Description("poly")]
            Poly,

            [Description("rect")]
            Rect,
        }

        #endregion


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _altAttribute = new SimpleSingleTypeAttribute<Text>("alt");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Coords> _coordAttribute = new SimpleSingleTypeAttribute<Coords>("coords");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _downloadAttribute = new SimpleSingleTypeAttribute<URI>("download");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _hrefAttribute = new SimpleSingleTypeAttribute<URI>("href");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<LanguageCode> _hrefLangAttribute = new SimpleSingleTypeAttribute<LanguageCode>("hreflang");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<MediaDescriptions> _mediaAttribute = new SimpleSingleTypeAttribute<MediaDescriptions>("media");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _noHRefAttribute = new FlagTypeAttribute("nohref");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly ValuesSelectionTypeAttribute<Text> _relationAttribute = new ValuesSelectionTypeAttribute<Text>("rel",typeof(RelAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _shapeAttribute = new ValuesSelectionTypeAttribute<Text>("shape",typeof(ShapeAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<TargetType> _targetAttribute = new SimpleSingleTypeAttribute<TargetType>("target");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<MIME_Type> _typeAttribute = new SimpleSingleTypeAttribute<MIME_Type>("type");


        public Area(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies that an area has no associated link
        /// Not supported in HTML5
        /// </summary>
        public IAttributeDataAccess NoHRef { get { return _noHRefAttribute; } }


        /// <summary>
        /// Alternate text. This attribute is required.
        /// </summary>
        public IAttributeDataAccess Alt { get { return _altAttribute; } }


        /// <summary>
        /// Specifies the position and shape on the screen. 
        /// The number and order of values depends on the shape being defined. 
        /// Possible combinations:
        /// * rect: left-x, top-y, right-x, bottom-y.
        /// * circle: center-x, center-y, radius.
        /// * poly: x1, y1, x2, y2, ..., xN, yN. 
        /// The first and the last x and y coordinate pair should be the same, in order to close the polygon.
        /// Coordinates are relative to the top-left corner of the object. All values are separated by commas.
        /// </summary>
        public IAttributeDataAccess Coords { get { return _coordAttribute; } }

        /// <summary>
        /// Specifies that the target will be downloaded when a user clicks on the hyperlink
        /// </summary>
        public IAttributeDataAccess Download { get { return _downloadAttribute; } }

        /// <summary>
        /// Specifies the location of a Web resource.
        /// </summary>
        public IAttributeDataAccess HRef { get { return _hrefAttribute; } }

        /// <summary>
        /// Specifies the language of the target URL
        /// </summary>
        public IAttributeDataAccess HRefLang { get { return _hrefLangAttribute; } }

        /// <summary>
        /// Specifies what media/device the target URL is optimized for
        /// </summary>
        public IAttributeDataAccess Media { get { return _mediaAttribute; } }

        /// <summary>
        /// The rel attribute specifies the relationship between the current document and the linked document.
        /// Only used if the href attribute is present.
        /// </summary>
        public IAttributeDataAccess Rel { get { return _relationAttribute; } }

        /// <summary>
        /// Specifies the shape of a region. 
        /// Possible values are:
        ///  * default: Specifies the entire region
        /// * rect: Defines a rectangular region.
        /// * circle: Defines a circular region.
        /// * poly: Defines a polygonal region.
        /// </summary>
        public IAttributeDataAccess Shape { get { return _shapeAttribute; } }

        /// <summary>
        /// Specifies where to open the target URL
        /// </summary>
        public IAttributeDataAccess Target { get { return _targetAttribute; } }

        /// <summary>
        /// Specifies the MIME type of the target URL
        /// </summary>
        public IAttributeDataAccess Type { get { return _typeAttribute; } }


        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public  override bool IsValid()
        {
            return (_altAttribute.HasValue());
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}