
namespace HTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class DeclareAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "declare";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}
