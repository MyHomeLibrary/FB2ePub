namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The kbd element indicates input to be entered by the user.
    /// </summary>
    [HTMLItemAttribute(ElementName = "kbd", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Kbd : TextBasedElement
    {
        public Kbd(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
