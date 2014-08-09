
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnMouseDownEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmousedown";
        }

        #endregion
    }
}