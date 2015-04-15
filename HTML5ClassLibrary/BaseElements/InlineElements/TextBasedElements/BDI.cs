namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// bdi stands for Bi-directional Isolation.
    /// The "bdi" tag isolates a part of text that might be formatted in a different direction from other text outside it.
    /// This element is useful when embedding user-generated content with an unknown directionality
    /// </summary>
    [HTMLItemAttribute(ElementName = "bdi", SupportedStandards = HTMLElementType.HTML5)]
    public class BDI : TextBasedElement
    {
        public BDI(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        public override object Clone()
        {
            var item = new BDI(HTMLStandard);
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
