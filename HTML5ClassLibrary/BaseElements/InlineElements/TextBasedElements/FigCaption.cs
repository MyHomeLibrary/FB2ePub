namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "figcaption" tag defines a caption for a "figure" element.
    /// The "figcaption" element can be placed as the first or last child of the "figure" element.
    /// </summary>
    public class FigCaption : TextBasedElement
    {
        public const string ElementName = "figcaption";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
