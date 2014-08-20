using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
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
        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _disabledAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        /// <summary>
        /// Specifies that a group of related form elements should be disabled
        /// </summary>
        public FlagAttribute Disabled { get { return _disabledAttribute; }}

        /// <summary>
        /// Specifies one or more forms the fieldset belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies a name for the fieldset
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}

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
