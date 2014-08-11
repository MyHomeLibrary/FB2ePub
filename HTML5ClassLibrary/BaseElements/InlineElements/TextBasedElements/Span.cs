namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The span element offers a generic way of adding structure to content.
    /// </summary>
    public class Span : TextBasedElement
    {
        internal const string ElementName = "span";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
