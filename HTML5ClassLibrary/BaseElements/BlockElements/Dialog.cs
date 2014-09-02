using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The "dialog" tag defines a dialog box or window.
    ///The "dialog" element makes it easy to create popup dialogs and modals on a web page.
    ///</summary>
    [HTMLItemAttribute(ElementName = "dialog", SupportedStandards = HTMLElementType.HTML5 )]
    public class Dialog: HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _openAttribute = new FlagTypeAttribute("open");


        public Dialog(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies that the dialog should be visible (open) to the user
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
