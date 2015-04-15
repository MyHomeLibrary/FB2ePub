namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "big" tag defines bigger text.
    /// </summary>
    [HTMLItemAttribute(ElementName = "big", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class BigText : TextBasedElement
    {
        public BigText(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        public override object Clone()
        {
            var item = new BigText(HTMLStandard);
            item.CloneAttributes(this);
            foreach (var htmlItem in Subitems)
            {
                item.Add(htmlItem.Clone() as IHTMLItem);
            }
            item.TextContent = TextContent;
            return item;
        }


    }
}
