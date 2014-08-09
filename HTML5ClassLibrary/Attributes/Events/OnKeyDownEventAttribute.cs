
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnKeyDownEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onkeydown";
        }

        #endregion
    }
}