namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The dfn element contains the defining instance of the enclosed term.
    /// </summary>
    [HTMLItemAttribute(ElementName = "dfn", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Definition : TextBasedElement
    {
        public Definition(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        public override object Clone()
        {
            var item = new Definition(HTMLStandard);
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
