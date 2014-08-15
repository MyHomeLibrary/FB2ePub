
namespace HTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class DisabledAttribute : BaseFlagAttribute
    {

        private const string AttributeName = "disabled";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}