
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnMouseUpEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmouseup";
        }

        #endregion
    }
}