namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnSubmitEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onsubmit";
        }

        #endregion

    }
}
