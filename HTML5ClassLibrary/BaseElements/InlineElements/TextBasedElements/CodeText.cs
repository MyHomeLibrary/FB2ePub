namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The code element contains a fragment of computer code.
    /// </summary>
    [HTMLItemAttribute(ElementName = "code", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class CodeText : TextBasedElement
    {
        public CodeText(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
