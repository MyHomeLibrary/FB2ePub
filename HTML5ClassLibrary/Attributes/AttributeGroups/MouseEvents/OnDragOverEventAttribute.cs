using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// Script to be run when an element is being dragged over a valid drop target
    /// </summary>
    public class OnDragOverEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "ondragover";
        }

        #endregion
    }
}
