namespace HTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The i element renders text in italic style.
    /// Although the i element is part of the XHTML specification, 
    /// its use is discouraged, since it has no semantic meaning and is only used for formatting. 
    /// Equivalent formatting can be achieved using CSS.
    /// </summary>
    [HTMLItemAttribute(ElementName = "i", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class ItalicText : TextBasedElement
    {
    }
}
