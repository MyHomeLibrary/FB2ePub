namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "s" element is redefined in HTML5, and is now used to define text that is no longer correct.
    /// </summary>
    [HTMLItemAttribute(ElementName = "s", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.FrameSet)]    
    public class SElement : TextBasedElement
    {
        public SElement(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
