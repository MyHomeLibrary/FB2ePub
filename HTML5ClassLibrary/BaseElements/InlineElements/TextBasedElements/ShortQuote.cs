using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "q" tag defines a short quotation.
    /// Browsers normally insert quotation marks around the quotation
    /// </summary>
    public class ShortQuote : TextBasedElement
    {
        public const string ElementName = "q";


        private readonly CiteAttribute _citeAttribute = new CiteAttribute();


        /// <summary>
        /// 
        /// </summary>
        public CiteAttribute Cite { get { return _citeAttribute; }}

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _citeAttribute.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _citeAttribute.ReadAttribute(xElement);
        }

        #endregion
    }
}
