namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "samp" tag is a phrase tag. It defines sample output from a computer program.
    /// Tip: This tag is not deprecated, but it is possible to achieve richer effect with CSS.
    /// </summary>
    public class Sample : TextBasedElement
    {
        public const string ElementName = "samp";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion

    }
}
