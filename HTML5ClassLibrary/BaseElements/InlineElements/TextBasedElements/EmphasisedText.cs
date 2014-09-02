namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The em element is used to indicate emphasis.
    /// </summary>
    [HTMLItemAttribute(ElementName = "em", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class EmphasisedText : TextBasedElement
    {
        public EmphasisedText(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
