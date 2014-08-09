
namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnSelectEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onselect";
        }

        #endregion

    }
}