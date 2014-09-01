using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;
using XHTMLClassLibrary.BaseElements.Legends;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The fieldset element adds structure to forms by grouping together related controls and labels.
    /// </summary>
    [HTMLItemAttribute(ElementName = "fieldset", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class FieldSet : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _disabledAttribute = new FlagTypeAttribute("disabled");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _formIdAttribute = new SimpleSingleTypeAttribute<URI>("form");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>("name");

        /// <summary>
        /// Specifies that a group of related form elements should be disabled
        /// </summary>
        public IAttributeDataAccess Disabled { get { return _disabledAttribute; }}

        /// <summary>
        /// Specifies one or more forms the fieldset belongs to
        /// </summary>
        public IAttributeDataAccess Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies a name for the fieldset
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; }}

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            if (item is Legend)
            {
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
