
namespace XHTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class ReadOnlyAttribute : BaseFlagAttribute
    {

        private const string AttributeName = "readonly";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}