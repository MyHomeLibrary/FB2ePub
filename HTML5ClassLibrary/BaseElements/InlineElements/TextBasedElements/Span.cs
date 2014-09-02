namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The span element offers a generic way of adding structure to content.
    /// </summary>
    [HTMLItemAttribute(ElementName = "span", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Span : TextBasedElement
    {
        public Span(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
