using System.Xml.Linq;
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
        public ShortQuote()
        {
            RegisterAttribute(_citeAttribute);
        }

        private readonly CiteAttribute _citeAttribute = new CiteAttribute();


        /// <summary>
        /// 
        /// </summary>
        public CiteAttribute Cite { get { return _citeAttribute; }}
    }
}
