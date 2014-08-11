using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when the playing position has changed (like when the user fast forwards to a different point in the media)
    /// </summary>
    public class OnMediaTimeUpdateEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "ontimeupdate";
        }

        #endregion
    }
}
