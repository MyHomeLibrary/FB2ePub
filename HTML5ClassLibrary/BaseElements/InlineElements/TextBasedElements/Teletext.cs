namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    [HTMLItemAttribute(ElementName = "tt", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Teletext : TextBasedElement
    {
        public Teletext(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        public override object Clone()
        {
            var item = new Teletext(HTMLStandard);
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
