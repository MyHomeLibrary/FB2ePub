namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// An abbreviation is a shortened form of a word or phrase. 
    /// The abbr element is used to identify an abbreviation,
    /// and can help assistive technologies to correctly pronounce abbreviated text.
    /// </summary>
    public class Abbreviation : TextBasedElement
    {
        public const string ElementName = "abbr";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
