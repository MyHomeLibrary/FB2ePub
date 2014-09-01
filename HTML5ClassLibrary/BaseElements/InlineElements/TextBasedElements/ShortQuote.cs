using XHTMLClassLibrary.AttributeDataTypes;
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
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _citeAttribute = new SimpleSingleTypeAttribute<URI>("cite");


        /// <summary>
        /// 
        /// </summary>
        public IAttributeDataAccess Cite { get { return _citeAttribute; }}
    }
}
