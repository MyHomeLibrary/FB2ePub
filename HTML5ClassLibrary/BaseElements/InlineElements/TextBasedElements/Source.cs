using XHTMLClassLibrary.AttributeDataTypes;
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
        private readonly SimpleSingleTypeAttribute<MediaDescriptions> _mediaAttrib = new SimpleSingleTypeAttribute<MediaDescriptions>("media");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<MIME_Type> _mimeType = new SimpleSingleTypeAttribute<MIME_Type>("type");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _srcAttrib = new SimpleSingleTypeAttribute<URI>("src");

        public IAttributeDataAccess Src
        {
            get { return _srcAttrib; }
        }

        public IAttributeDataAccess Media
        {
            get { return _mediaAttrib; }
        }

        public IAttributeDataAccess Type
        {
            get { return _mimeType; }
        }
    }
}
