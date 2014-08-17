using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;

namespace XHTMLClassLibrary.BaseElements.MapAreas
{
    /// <summary>
    /// The area element identifies geometric regions of a client-side image map, and provides a hyperlink for each region.
    /// </summary>
    [HTMLItemAttribute(ElementName = "area", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Area : HTMLItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AltAttribute _altAttribute = new AltAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CoordinatesAttribute _coordAttribute = new CoordinatesAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly DownloadAttribute  _downloadAttribute = new DownloadAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HrefAttribute _hrefAttribute = new HrefAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly HRefLanguageAttribute _hrefLangAttribute = new HRefLanguageAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MediaAttribute _mediaAttribute = new MediaAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NoHRefAttribute _noHRefAttribute = new NoHRefAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 )]
        private readonly AreaRelationAttribute _relationAttribute = new AreaRelationAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ShapeAttribute _shapeAttribute = new ShapeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FormTargetAttribute _targetAttribute = new FormTargetAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 )]
        private readonly MIMETypeAttribute  _typeAttribute = new MIMETypeAttribute();




        /// <summary>
        /// Specifies that an area has no associated link
        /// Not supported in HTML5
        /// </summary>
        public NoHRefAttribute NoHRef { get { return _noHRefAttribute; }}


        /// <summary>
        /// Alternate text. This attribute is required.
        /// </summary>
        public AltAttribute Alt { get { return _altAttribute; } }


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
        public CoordinatesAttribute Coords { get { return _coordAttribute; } }

        /// <summary>
        /// Specifies that the target will be downloaded when a user clicks on the hyperlink
        /// </summary>
        public DownloadAttribute Download { get { return _downloadAttribute; }}

        /// <summary>
        /// Specifies the location of a Web resource.
        /// </summary>
        public HrefAttribute HRef { get { return _hrefAttribute; } }

        /// <summary>
        /// Specifies the language of the target URL
        /// </summary>
        public HRefLanguageAttribute HRefLang { get { return _hrefLangAttribute; }}

        /// <summary>
        /// Specifies what media/device the target URL is optimized for
        /// </summary>
        public MediaAttribute Media { get { return _mediaAttribute; }}

        /// <summary>
        /// The rel attribute specifies the relationship between the current document and the linked document.
        /// Only used if the href attribute is present.
        /// </summary>
        public AreaRelationAttribute Rel { get { return _relationAttribute; } }

        /// <summary>
        /// Specifies the shape of a region. 
        /// Possible values are:
        ///  * default: Specifies the entire region
        /// * rect: Defines a rectangular region.
        /// * circle: Defines a circular region.
        /// * poly: Defines a polygonal region.
        /// </summary>
        public ShapeAttribute Shape { get { return _shapeAttribute; } }

        /// <summary>
        /// Specifies where to open the target URL
        /// </summary>
        public FormTargetAttribute Target { get { return _targetAttribute; } }

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