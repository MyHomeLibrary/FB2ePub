namespace HTML5ClassLibrary.Attributes.Events
{
    public class OnDblClickEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "ondblclick";
        }

        #endregion
    }
}