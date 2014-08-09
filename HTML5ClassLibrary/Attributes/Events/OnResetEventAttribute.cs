namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnResetEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onreset";
        }

        #endregion
    }
}
