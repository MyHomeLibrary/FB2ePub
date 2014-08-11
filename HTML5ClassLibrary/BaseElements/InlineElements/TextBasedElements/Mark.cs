namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "mark" tag defines marked text.
    /// Use the "mark" tag if you want to highlight parts of your text.
    /// </summary>
    public class Mark : TextBasedElement
    {
        public const string ElementName = "mark";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }
        #endregion
    }
}
