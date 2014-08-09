
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnClickEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onclick";
        }

        #endregion
    }
}