using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// Script to be run when a context menu is triggered
    /// </summary>
    public class OnContextMenuAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "oncontextmenu";
        }

        #endregion
    }
}
