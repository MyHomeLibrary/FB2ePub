using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when the media has paused but is expected to resume (like when the media pauses to buffer more data)
    /// </summary>
    public class OnMediaWaitingEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onwaiting";
        }

        #endregion
    }
}
