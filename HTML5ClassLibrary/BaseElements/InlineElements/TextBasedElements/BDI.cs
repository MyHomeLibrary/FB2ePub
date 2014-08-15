namespace HTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// bdi stands for Bi-directional Isolation.
    /// The "bdi" tag isolates a part of text that might be formatted in a different direction from other text outside it.
    /// This element is useful when embedding user-generated content with an unknown directionality
    /// </summary>
    [HTMLItemAttribute(ElementName = "bdi", SupportedStandards = HTMLElementType.HTML5)]
    public class BDI : TextBasedElement
    {
    }
}
