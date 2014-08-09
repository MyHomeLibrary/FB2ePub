
namespace HTML5ClassLibrary.Attributes.FlaggedAttributes
{
    public class NoHRefAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "nohref";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion

    }
}