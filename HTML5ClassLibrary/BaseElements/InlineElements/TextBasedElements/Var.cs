namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The var element is used to indicate an instance of a computer code variable or program argument.
    /// </summary>
    [HTMLItemAttribute(ElementName = "var", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Var : TextBasedElement
    {
        public Var(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
