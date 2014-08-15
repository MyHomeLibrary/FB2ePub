
namespace HTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class CheckedAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "checked";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}