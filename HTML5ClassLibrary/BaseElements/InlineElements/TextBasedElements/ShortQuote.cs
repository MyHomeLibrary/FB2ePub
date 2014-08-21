using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "q" tag defines a short quotation.
    /// Browsers normally insert quotation marks around the quotation
    /// </summary>
    [HTMLItemAttribute(ElementName = "q", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class ShortQuote : TextBasedElement
    {
        [AttributeTypeAttributeMember(Name = "cite", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _citeAttribute = new URITypeAttribute();


        /// <summary>
        /// 
        /// </summary>
        public URITypeAttribute Cite { get { return _citeAttribute; }}
    }
}
