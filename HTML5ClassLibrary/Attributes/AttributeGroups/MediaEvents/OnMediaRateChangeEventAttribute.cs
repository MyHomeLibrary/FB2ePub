using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run each time the playback rate changes (like when a user switches to a slow motion or fast forward mode)
    /// </summary>
    public class OnMediaRateChangeEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onratechange";
        }

        #endregion
    }
}
