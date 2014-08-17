
namespace XHTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class SelectedAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "selected";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}
