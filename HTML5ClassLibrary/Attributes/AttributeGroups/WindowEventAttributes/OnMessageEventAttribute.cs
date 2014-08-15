using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// Script to be run when the message is triggered
    /// </summary>
    public class OnMessageEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmessage";
        }

        #endregion
    }
}
