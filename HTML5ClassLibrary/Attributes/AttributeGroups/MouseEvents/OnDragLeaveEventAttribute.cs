using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// Script to be run when an element leaves a valid drop target
    /// </summary>
    public class OnDragLeaveEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "ondragleave";
        }

        #endregion
    }
}
