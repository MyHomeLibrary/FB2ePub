
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnLoadEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onload";
        }

        #endregion
    }
}
