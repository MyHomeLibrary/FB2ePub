using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.Ruby
{
    /// <summary>
    /// The rt element contains ruby text annotations.
    /// </summary>
    [HTMLItemAttribute(ElementName = "rt", SupportedStandards = HTMLElementType.HTML5)]
    public class RtElement : HTMLItem , IRubyItem
    {

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            if (item is IInlineItem)
            {
                if (item is RubyElement)
                {
                    return false;
                }
                return item.IsValid();
            }
            return false;
        }

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}
