using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when the seeking attribute is set to false indicating that seeking has ended
    /// </summary>
    public class OnMediaSeekedEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onseeked";
        }

        #endregion
    }
}
