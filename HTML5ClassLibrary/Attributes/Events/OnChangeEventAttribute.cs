
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnChangeEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onchange";
        }

        #endregion

    }
}