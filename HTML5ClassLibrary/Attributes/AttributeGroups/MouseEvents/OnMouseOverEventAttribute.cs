
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnMouseOverEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmouseover";
        }

        #endregion
    }
}