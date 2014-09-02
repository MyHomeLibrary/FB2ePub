namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "u" tag represents some text that should be stylistically different from normal text, such as misspelled words or proper nouns in Chinese.
    /// </summary>
    [HTMLItemAttribute(ElementName = "u", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Underlined : TextBasedElement
    {
        public Underlined(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
