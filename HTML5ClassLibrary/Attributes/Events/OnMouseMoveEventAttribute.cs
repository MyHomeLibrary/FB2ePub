
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnMouseMoveEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmousemove";
        }

        #endregion
    }
}