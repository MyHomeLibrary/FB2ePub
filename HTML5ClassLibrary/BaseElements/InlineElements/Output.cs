using System.Collections.Generic;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "output" tag represents the result of a calculation (like one performed by a script).
    /// </summary>
    [HTMLItemAttribute(ElementName = "output", SupportedStandards = HTMLElementType.HTML5)]
    public class Output : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<NameTokens> _forAttribute = new SimpleSingleTypeAttribute<NameTokens>("for");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _formIdAttribute = new SimpleSingleTypeAttribute<URI>("form");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>("name");


        /// <summary>
        /// Specifies the relationship between the result of the calculation, and the elements used in the calculation
        /// </summary>
        public IAttributeDataAccess For { get { return _forAttribute; } }

        /// <summary>
        /// Specifies one or more forms the output element belongs to
        /// </summary>
        public IAttributeDataAccess Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies a name for the output element
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; }}

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
