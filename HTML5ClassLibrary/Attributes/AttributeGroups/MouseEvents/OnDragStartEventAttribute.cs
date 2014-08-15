using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// Script to be run at the start of a drag operation
    /// </summary>
    public  class OnDragStartEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "ondragstart";
        }

        #endregion
    }
}
