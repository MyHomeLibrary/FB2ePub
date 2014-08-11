using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when something bad happens and the file is suddenly unavailable (like unexpectedly disconnects)
    /// </summary>
    public class OnMediaEmptiedEventAttribute: OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onemptied";
        }

        #endregion
    }
}
