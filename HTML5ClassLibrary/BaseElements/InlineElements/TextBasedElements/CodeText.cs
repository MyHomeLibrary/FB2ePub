namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The code element contains a fragment of computer code.
    /// </summary>
    public class CodeText : TextBasedElement
    {
        public const string ElementName = "code";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
