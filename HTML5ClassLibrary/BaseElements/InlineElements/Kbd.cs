namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The kbd element indicates input to be entered by the user.
    /// </summary>
    public class Kbd : TextBasedElement
    {
        public const string ElementName = "kbd";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
