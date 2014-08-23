using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// 
    /// </summary>
    [HTMLItemAttribute(ElementName = "source", SupportedStandards = HTMLElementType.HTML5)]
    public class Source : TextBasedElement
    {
        [AttributeTypeAttributeMember(Name = "media", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MediaDescriptionsTypeAttribute _mediaAttrib = new MediaDescriptionsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MIMETypeAttribute _mimeType = new MIMETypeAttribute();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _srcAttrib = new URITypeAttribute();

        public URITypeAttribute Src
        {
            get { return _srcAttrib; }
        }

        public MediaDescriptionsTypeAttribute Media
        {
            get { return _mediaAttrib; }
        }

        public MIMETypeAttribute Type
        {
            get { return _mimeType; }
        }
    }
}
