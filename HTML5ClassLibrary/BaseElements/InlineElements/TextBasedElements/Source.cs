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
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MediaAttribute _mediaAttrib = new MediaAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MIMETypeAttribute _mimeType = new MIMETypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SourceAttribute _srcAttrib = new SourceAttribute();

        public SourceAttribute Src
        {
            get { return _srcAttrib; }
        }

        public MediaAttribute Media
        {
            get { return _mediaAttrib; }
        }

        public MIMETypeAttribute Type
        {
            get { return _mimeType; }
        }
    }
}
