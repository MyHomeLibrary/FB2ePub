namespace XHTMLClassLibrary.Attributes.FlaggedAttributes
{
    /// <summary>
    /// The nowrap attribute is a boolean attribute.
    /// When present, it specifies that the content inside a cell should not wrap.
    /// </summary>
    public class NoWrapAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "nowrap";

        public override string GetElementName()
        {
            return AttributeName;
        }
    }
}
