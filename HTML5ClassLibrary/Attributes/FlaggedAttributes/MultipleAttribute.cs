
namespace HTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class MultipleAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "multiple";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion

    }
}
