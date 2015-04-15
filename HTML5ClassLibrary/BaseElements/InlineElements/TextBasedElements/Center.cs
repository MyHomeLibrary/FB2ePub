namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{    
    /// <summary>
    /// The "center" tag is used to center-align text.
    /// </summary>
    [HTMLItem(ElementName = "center", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class Center :TextBasedElement
    {
        public Center(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        public override object Clone()
        {
            var item = new Center(HTMLStandard);
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
