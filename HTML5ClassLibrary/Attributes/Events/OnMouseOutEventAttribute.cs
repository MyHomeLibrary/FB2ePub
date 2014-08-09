
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnMouseOutEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmouseout";
        }

        #endregion
    }
}