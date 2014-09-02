namespace XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements
{    
    /// <summary>
    /// The "center" tag is used to center-align text.
    /// </summary>
    [HTMLItem(ElementName = "center", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class Center :TextBasedElement
    {
        public Center(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }
    }
}
