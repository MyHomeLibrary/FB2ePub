using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// 
    /// </summary>
    public class OnMouseEnterEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "mouseenter";
        }
        #endregion
    }
}
