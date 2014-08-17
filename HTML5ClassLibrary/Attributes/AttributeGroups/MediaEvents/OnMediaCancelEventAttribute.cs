using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
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
