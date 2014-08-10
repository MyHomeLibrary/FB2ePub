namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The strong element is used to indicate stronger emphasis.
    /// </summary>
    public class Strong : TextBasedElement
    {
        internal const string ElementName = "strong";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
