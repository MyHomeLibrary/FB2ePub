namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// An abbreviation is a shortened form of a word or phrase. 
    /// The abbr element is used to identify an abbreviation,
    /// and can help assistive technologies to correctly pronounce abbreviated text.
    /// </summary>
    [HTMLItemAttribute(ElementName = "abbr", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Abbreviation : TextBasedElement
    {
        public Abbreviation(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
