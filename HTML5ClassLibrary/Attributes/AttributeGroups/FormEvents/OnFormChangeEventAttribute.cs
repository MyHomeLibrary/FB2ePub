using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// Script to be run when a form changes
    /// </summary>
    public class OnFormChangeEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onformchange";
        }

        #endregion
    }
}
