namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The b element renders text in element style.
    /// Although the b element is part of the XHTML specification, its use is discouraged. 
    /// The element has no semantic meaning and is only used for formatting. 
    /// Equivalent formatting can be achieved using CSS.
    /// </summary>
    [HTMLItemAttribute(ElementName = "b", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class BoldText : TextBasedElement
    {
    }
}
