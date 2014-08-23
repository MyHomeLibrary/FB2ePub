using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "output" tag represents the result of a calculation (like one performed by a script).
    /// </summary>
    [HTMLItemAttribute(ElementName = "output", SupportedStandards = HTMLElementType.HTML5)]
    public class Output : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "for", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly NameTokensTypeAttribute _forAttribute = new NameTokensTypeAttribute();

        [AttributeTypeAttributeMember(Name = "form", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _formIdAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _nameAttribute = new TextValueTypeAttribute();


        /// <summary>
        /// Specifies the relationship between the result of the calculation, and the elements used in the calculation
        /// </summary>
        public NameTokensTypeAttribute For { get { return _forAttribute; } }

        /// <summary>
        /// Specifies one or more forms the output element belongs to
        /// </summary>
        public URITypeAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies a name for the output element
        /// </summary>
        public TextValueTypeAttribute Name { get { return _nameAttribute; }}

        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
