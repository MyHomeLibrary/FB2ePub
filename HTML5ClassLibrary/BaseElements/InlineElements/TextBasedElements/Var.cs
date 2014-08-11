namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The var element is used to indicate an instance of a computer code variable or program argument.
    /// </summary>
    public class Var : TextBasedElement
    {
        internal const string ElementName = "var";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
