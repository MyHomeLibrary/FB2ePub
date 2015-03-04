namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    public abstract class TextBasedElement : HTMLItem, IInlineItem
    {
        protected TextBasedElement(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            if (item is IInlineItem)
            {
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
