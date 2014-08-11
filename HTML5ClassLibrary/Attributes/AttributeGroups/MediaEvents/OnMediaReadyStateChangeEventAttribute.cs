using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run each time the ready state changes (the ready state tracks the state of the media data)
    /// </summary>
    public class OnMediaReadyStateChangeEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onreadystatechange";
        }

        #endregion
    }
}
