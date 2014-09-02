namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "acronym" tag defines an acronym.
    /// An acronym can be spoken as if it were a word, example NATO, NASA, ASAP, GUI.
    /// By marking up acronyms you can give useful information to browsers, spell checkers, translation systems and search-engine indexers.
    /// </summary>
    [HTMLItemAttribute(ElementName = "acronym", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Acronym : TextBasedElement
    {
        public Acronym(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
