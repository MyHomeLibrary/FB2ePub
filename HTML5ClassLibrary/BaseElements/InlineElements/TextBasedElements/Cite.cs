namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    /// <summary>
    /// The "cite" tag defines the title of a work (e.g. a book, a song, a movie, a TV show, a painting, a sculpture, etc.).
    /// Note: A person's name is not the title of a work.
    /// </summary>
    public class Cite : TextBasedElement
    {
        public const string ElementName = "cite";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
