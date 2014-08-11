namespace HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements
{
    public class BDI : TextBasedElement
    {
        internal const string ElementName = "bdi";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
