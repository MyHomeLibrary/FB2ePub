using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when the seeking attribute is set to true indicating that seeking is active
    /// </summary>
    public class OnMediaSeekingEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onseeking";
        }

        #endregion
    }
}
