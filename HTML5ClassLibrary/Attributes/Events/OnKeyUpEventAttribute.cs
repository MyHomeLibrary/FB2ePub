
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnKeyUpEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onkeyup";
        }

        #endregion
    }
}