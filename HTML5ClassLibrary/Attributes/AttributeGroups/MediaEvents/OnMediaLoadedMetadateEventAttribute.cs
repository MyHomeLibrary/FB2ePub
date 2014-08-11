using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when meta data (like dimensions and duration) are loaded
    /// </summary>
    public class OnMediaLoadedMetadateEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onloadedmetadata";
        }

        #endregion
    }
}
