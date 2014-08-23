using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "details" tag specifies additional details that the user can view or hide on demand.
    /// The "details" tag can be used to create an interactive widget that the user can open and close. Any sort of content can be put inside the "details" tag.
    /// The content of a "details" element should not be visible unless the open attribute is set.
    /// </summary>
    [HTMLItemAttribute(ElementName = "details", SupportedStandards = HTMLElementType.HTML5)]
    public class Details : HTMLItem, IBlockElement 
    {
        [AttributeTypeAttributeMember(Name = "open", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _openAttribute = new FlagTypeAttribute();


        /// <summary>
        /// Specifies that the details should be visible (open) to the user
        /// </summary>
        public FlagTypeAttribute Open { get { return _openAttribute; }}

        public override bool IsValid()
        {
            return true;
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }

    }
}
