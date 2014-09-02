namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The sub element indicates that its contents should be regarded as a subscript.
    /// </summary>
        [HTMLItemAttribute(ElementName = "sub", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Sub : TextBasedElement
    {
        public Sub(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
