using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when the length of the media changes
    /// </summary>
    public class OnMediaDurationChangeEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "ondurationchange";
        }

        #endregion
    }
}
