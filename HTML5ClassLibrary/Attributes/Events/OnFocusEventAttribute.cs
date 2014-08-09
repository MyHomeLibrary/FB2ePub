
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnFocusEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onfocus";
        }

        #endregion
    }
}