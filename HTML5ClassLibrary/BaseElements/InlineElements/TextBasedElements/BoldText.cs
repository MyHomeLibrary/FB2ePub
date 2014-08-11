namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The b element renders text in element style.
    /// Although the b element is part of the XHTML specification, its use is discouraged. 
    /// The element has no semantic meaning and is only used for formatting. 
    /// Equivalent formatting can be achieved using CSS.
    /// </summary>
    public class BoldText : TextBasedElement
    {
        internal const string ElementName = "b";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
