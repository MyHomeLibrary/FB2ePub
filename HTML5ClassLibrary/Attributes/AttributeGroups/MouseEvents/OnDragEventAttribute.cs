using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// Script to be run when an element is dragged
    /// </summary>
    public class OnDragEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "ondrag";
        }

        #endregion
    }
}
