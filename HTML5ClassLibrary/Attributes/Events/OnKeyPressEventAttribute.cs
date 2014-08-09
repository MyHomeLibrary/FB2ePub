
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnKeyPressEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onkeypress";
        }

        #endregion
    }
}