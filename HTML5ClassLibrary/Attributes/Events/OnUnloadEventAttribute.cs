namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnUnloadEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onunload";
        }

        #endregion
    }
}
