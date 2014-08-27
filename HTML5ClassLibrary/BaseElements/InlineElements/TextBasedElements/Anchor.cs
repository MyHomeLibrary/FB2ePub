using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// Hyperlink
    /// </summary>
    [HTMLItemAttribute(ElementName = "a", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Anchor : TextBasedElement
    {
        [AttributeTypeAttributeMember(Name = "charset", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Charset> _charsetAttribute = new SimpleSingleTypeAttribute<Charset>();

        [AttributeTypeAttributeMember(Name = "coords", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Coords> _coordinatesAttribute = new SimpleSingleTypeAttribute<Coords>();

        [AttributeTypeAttributeMember(Name = "download", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _downloadAttrib = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "href", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _hrefAttrib = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "hreflang", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<LanguageCode> _hrefLangAttrib = new SimpleSingleTypeAttribute<LanguageCode>();

        [AttributeTypeAttributeMember(Name = "media", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<MediaDescriptions> _mediaAttr = new SimpleSingleTypeAttribute<MediaDescriptions>();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "rel", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _relAttrib = new ValuesSelectionTypeAttribute<Text>("alternate;author;bookmark;help;license;next;nofollow;norefferer;prefetch;prev;search;tag");

        [AttributeTypeAttributeMember(Name = "rev", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<LinkTypes> _reverseRelationAttribute = new SimpleSingleTypeAttribute<LinkTypes>();

        [AttributeTypeAttributeMember(Name = "shape", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _shapeAttribute = new ValuesSelectionTypeAttribute<Text>("circle;default;poly;rect");

        [AttributeTypeAttributeMember(Name = "target", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<TargetType> _targetAttr = new SimpleSingleTypeAttribute<TargetType>();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<MIME_Type> _typeAttr = new SimpleSingleTypeAttribute<MIME_Type>();
 


#region public_attributes

        /// <summary>
        /// Specifies the shape of a link
        /// Not supported in HTML5
        /// </summary>
        public IAttributeDataAccess Shape { get { return _shapeAttribute; }}

        /// <summary>
        /// Specifies the relationship between the linked document and the current document
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess ReverseRelation { get { return _reverseRelationAttribute; } }

        /// <summary>
        /// Specifies the name of an anchor
        /// Not supported in HTML5. Use the id attribute instead
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }

        /// <summary>
        /// Specifies the coordinates of a link
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Coordinates { get { return _coordinatesAttribute; } }

        /// <summary>
        ///  Specifies the character-set of a linked document
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Charset { get { return _charsetAttribute; } }

        /// <summary>
        /// This attribute specifies the location of a Web resource. 
        /// For example: http://xhtml.com/ or mailto:info@xhtml.com.
        /// </summary>
        public IAttributeDataAccess HRef { get { return _hrefAttrib; } }

        /// <summary>
        /// Specifies the primary language of the resource designated by href and may only be used when href is specified.
        /// </summary>
        public IAttributeDataAccess HrefLanguage { get { return _hrefLangAttrib; } }

        /// <summary>
        /// Describes the relationship from the current document to the resource specified by the href attribute. The value of this attribute is a space-separated list of link types. 
        /// For example: appendix.
        /// </summary>
        public IAttributeDataAccess Rel { get { return _relAttrib; } }

        /// <summary>
        /// Specifies that the target will be downloaded when a user clicks on the hyperlink
        /// </summary>
        public IAttributeDataAccess Download { get { return _downloadAttrib; } }

        /// <summary>
        /// Specifies what media/device the linked document is optimized for
        /// </summary>
        public IAttributeDataAccess Media { get { return _mediaAttr; } }

        /// <summary>
        /// Specifies where to open the linked document
        /// </summary>
        public IAttributeDataAccess Target { get { return _targetAttr; } }

        /// <summary>
        /// Specifies the MIME type of the linked document
        /// </summary>
        public IAttributeDataAccess Type { get { return _typeAttr; } }

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