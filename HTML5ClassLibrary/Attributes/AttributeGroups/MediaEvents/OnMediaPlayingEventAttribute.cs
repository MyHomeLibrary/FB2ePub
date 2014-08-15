using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when the media actually has started playing
    /// </summary>
    public class OnMediaPlayingEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onplaying";
        }

        #endregion
    }
}
