namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The i element renders text in italic style.
    /// Although the i element is part of the XHTML specification, 
    /// its use is discouraged, since it has no semantic meaning and is only used for formatting. 
    /// Equivalent formatting can be achieved using CSS.
    /// </summary>
    public class ItalicText : TextBasedElement
    {
        public const string ElementName = "i";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion

    }
}
