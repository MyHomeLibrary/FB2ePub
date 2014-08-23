using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.MapAreas
{
    /// <summary>
    /// The area element identifies geometric regions of a client-side image map, and provides a hyperlink for each region.
    /// </summary>
    [HTMLItemAttribute(ElementName = "area", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Area : HTMLItem
    {
        [AttributeTypeAttributeMember(Name = "alt", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _altAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "coords", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CoordinatesTypeAttribute _coordAttribute = new CoordinatesTypeAttribute();

        [AttributeTypeAttributeMember(Name = "download", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute  _downloadAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "href", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _hrefAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "hreflang", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly LanguageTypeAttribute _hrefLangAttribute = new LanguageTypeAttribute();

        [AttributeTypeAttributeMember(Name = "media", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MediaDescriptionsTypeAttribute _mediaAttribute = new MediaDescriptionsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "nohref", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _noHRefAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "rel", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly AreaRelationTypeAttribute _relationAttribute = new AreaRelationTypeAttribute();

        [AttributeTypeAttributeMember(Name = "shape", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ShapeTypeAttribute _shapeAttribute = new ShapeTypeAttribute();

        [AttributeTypeAttributeMember(Name = "target", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FormTargetTypeAttribute _targetAttribute = new FormTargetTypeAttribute();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MIMETypeAttribute  _typeAttribute = new MIMETypeAttribute();




        /// <summary>
        /// Specifies that an area has no associated link
        /// Not supported in HTML5
        /// </summary>
        public FlagTypeAttribute NoHRef { get { return _noHRefAttribute; }}


        /// <summary>
        /// Alternate text. This attribute is required.
        /// </summary>
        public TextValueTypeAttribute Alt { get { return _altAttribute; } }


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
        public CoordinatesTypeAttribute Coords { get { return _coordAttribute; } }

        /// <summary>
        /// Specifies that the target will be downloaded when a user clicks on the hyperlink
        /// </summary>
        public URITypeAttribute Download { get { return _downloadAttribute; }}

        /// <summary>
        /// Specifies the location of a Web resource.
        /// </summary>
        public URITypeAttribute HRef { get { return _hrefAttribute; } }

        /// <summary>
        /// Specifies the language of the target URL
        /// </summary>
        public LanguageTypeAttribute HRefLang { get { return _hrefLangAttribute; }}

        /// <summary>
        /// Specifies what media/device the target URL is optimized for
        /// </summary>
        public MediaDescriptionsTypeAttribute Media { get { return _mediaAttribute; }}

        /// <summary>
        /// The rel attribute specifies the relationship between the current document and the linked document.
        /// Only used if the href attribute is present.
        /// </summary>
        public AreaRelationTypeAttribute Rel { get { return _relationAttribute; } }

        /// <summary>
        /// Specifies the shape of a region. 
        /// Possible values are:
        ///  * default: Specifies the entire region
        /// * rect: Defines a rectangular region.
        /// * circle: Defines a circular region.
        /// * poly: Defines a polygonal region.
        /// </summary>
        public ShapeTypeAttribute Shape { get { return _shapeAttribute; } }

        /// <summary>
        /// Specifies where to open the target URL
        /// </summary>
        public FormTargetTypeAttribute Target { get { return _targetAttribute; } }

        /// <summary>
        /// Specifies the MIME type of the target URL
        /// </summary>
        public MIMETypeAttribute Type { get { return _typeAttribute; }}


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