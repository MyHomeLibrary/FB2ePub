using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
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