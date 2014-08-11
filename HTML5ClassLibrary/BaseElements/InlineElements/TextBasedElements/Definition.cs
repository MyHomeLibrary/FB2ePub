namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The dfn element contains the defining instance of the enclosed term.
    /// </summary>
    public class Definition : TextBasedElement
    {
        public const string ElementName = "dfn";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
