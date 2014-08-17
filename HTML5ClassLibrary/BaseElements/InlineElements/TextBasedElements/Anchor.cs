using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// Hyperlink
    /// </summary>
    [HTMLItemAttribute(ElementName = "a", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Anchor : TextBasedElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharsetAttribute _charsetAttribute = new CharsetAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private  readonly CoordinatesAttribute _coordinatesAttribute = new CoordinatesAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly DownloadAttribute _downloadAttrib = new DownloadAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HrefAttribute _hrefAttrib = new HrefAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HRefLanguageAttribute _hrefLangAttrib = new HRefLanguageAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MediaAttribute _mediaAttr = new MediaAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AreaRelationAttribute _relAttrib = new AreaRelationAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ReverseRelationAttribute _reverseRelationAttribute = new ReverseRelationAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ShapeAttribute _shapeAttribute = new ShapeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FormTargetAttribute _targetAttr = new FormTargetAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MIMETypeAttribute _typeAttr = new MIMETypeAttribute();
 


#region public_attributes

        /// <summary>
        /// Specifies the shape of a link
        /// Not supported in HTML5
        /// </summary>
        public ShapeAttribute Shape { get { return _shapeAttribute; }}

        /// <summary>
        /// Specifies the relationship between the linked document and the current document
        /// Not supported in HTML5.
        /// </summary>
        public ReverseRelationAttribute ReverseRelation { get { return _reverseRelationAttribute; }}

        /// <summary>
        /// Specifies the name of an anchor
        /// Not supported in HTML5. Use the id attribute instead
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}

        /// <summary>
        /// Specifies the coordinates of a link
        /// Not supported in HTML5.
        /// </summary>
        public CoordinatesAttribute Coordinates { get { return _coordinatesAttribute; }}

        /// <summary>
        ///  Specifies the character-set of a linked document
        /// Not supported in HTML5.
        /// </summary>
        public CharsetAttribute Charset { get { return _charsetAttribute; }}

        /// <summary>
        /// This attribute specifies the location of a Web resource. 
        /// For example: http://xhtml.com/ or mailto:info@xhtml.com.
        /// </summary>
        public HrefAttribute HRef { get { return _hrefAttrib; } }

        /// <summary>
        /// Specifies the primary language of the resource designated by href and may only be used when href is specified.
        /// </summary>
        public HRefLanguageAttribute HrefLanguage { get { return _hrefLangAttrib; } }

        /// <summary>
        /// Describes the relationship from the current document to the resource specified by the href attribute. The value of this attribute is a space-separated list of link types. 
        /// For example: appendix.
        /// </summary>
        public AreaRelationAttribute Rel { get { return _relAttrib; } }

        /// <summary>
        /// Specifies that the target will be downloaded when a user clicks on the hyperlink
        /// </summary>
        public DownloadAttribute Download {get { return _downloadAttrib; }}

        /// <summary>
        /// Specifies what media/device the linked document is optimized for
        /// </summary>
        public MediaAttribute Media { get { return _mediaAttr; }}

        /// <summary>
        /// Specifies where to open the linked document
        /// </summary>
        public FormTargetAttribute Target { get { return _targetAttr; }}

        /// <summary>
        /// Specifies the MIME type of the linked document
        /// </summary>
        public MIMETypeAttribute Type { get { return _typeAttr; }}

#endregion


        protected override  bool IsValidSubType(IHTMLItem item)
        {

            if (item is IInlineItem)
            {
                if (item is Anchor)
                {
                    return false;
                }
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
            {
                return true;
            }
            return false;
        }


        public override bool IsValid()
        {
            return HRef != null;
        }
    }
}