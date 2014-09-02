namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "cite" tag defines the title of a work (e.g. a book, a song, a movie, a TV show, a painting, a sculpture, etc.).
    /// Note: A person's name is not the title of a work.
    /// </summary>
    [HTMLItemAttribute(ElementName = "cite", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Cite : TextBasedElement
    {
        public Cite(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
