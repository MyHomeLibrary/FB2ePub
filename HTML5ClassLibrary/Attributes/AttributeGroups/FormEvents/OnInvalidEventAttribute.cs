using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// Script to be run when an element is invalid
    /// </summary>
    public class OnInvalidEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "oninvalid";
        }

        #endregion
    }
}
