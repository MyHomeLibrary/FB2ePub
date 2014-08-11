namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The em element is used to indicate emphasis.
    /// </summary>
    public class EmphasisedText : TextBasedElement
    {
        public const string ElementName = "em";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
