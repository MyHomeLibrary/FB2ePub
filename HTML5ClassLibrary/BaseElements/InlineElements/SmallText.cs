namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "small" tag defines smaller text (fine print and other side comments).
    /// </summary>
    public class SmallText : TextBasedElement
    {
        internal const string ElementName = "small";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
