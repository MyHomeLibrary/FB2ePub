namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The sup element indicates that its contents should regarded as superscript.
    /// </summary>
    public class Sup : TextBasedElement
    {
        internal const string ElementName = "sup";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion

    }
}
