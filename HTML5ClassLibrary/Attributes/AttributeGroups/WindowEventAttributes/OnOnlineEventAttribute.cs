using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// Script to be run when the document comes online
    /// </summary>
    public class OnOnlineEventAttribute: OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "ononline";
        }

        #endregion

    }
}
