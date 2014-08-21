using System.Xml.Linq;
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
        private readonly MediaDescriptionsAttribute _mediaAttrib = new MediaDescriptionsAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MIMETypeAttribute _mimeType = new MIMETypeAttribute();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _srcAttrib = new URITypeAttribute();

        public URITypeAttribute Src
        {
            get { return _srcAttrib; }
        }

        public MediaDescriptionsAttribute Media
        {
            get { return _mediaAttrib; }
        }

        public MIMETypeAttribute Type
        {
            get { return _mimeType; }
        }
    }
}
