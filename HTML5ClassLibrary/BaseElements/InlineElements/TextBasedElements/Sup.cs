namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The sup element indicates that its contents should regarded as superscript.
    /// </summary>
    [HTMLItemAttribute(ElementName = "sup", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Sup : TextBasedElement
    {
        public Sup(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
