namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The sub element indicates that its contents should be regarded as a subscript.
    /// </summary>
    public class Sub : TextBasedElement
    {
        internal const string ElementName = "sub";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
