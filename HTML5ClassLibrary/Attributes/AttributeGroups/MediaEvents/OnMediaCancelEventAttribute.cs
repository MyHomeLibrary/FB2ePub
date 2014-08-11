using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class OnMediaCancelEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "oncancel";
        }

        #endregion
    }
}
