using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// The onmouseout attribute fires when the mouse pointer moves out of an element.
    /// Note: The onmouseout attribute CANNOT be used with: "base", "bdo", "br", "head", "html", "iframe", "meta", "param", "script", "style", or "title".
    /// </summary>
    public class OnMouseOutEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmouseout";
        }

        #endregion
    }
}