
using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// The onmousemove attribute fires when the mouse pointer moves over an element.
    /// Note: The onmousemove attribute CANNOT be used with: "base", "bdo", "br", "head", "html", "iframe", "meta", "param", "script", "style", or "title".
    /// </summary>
    public class OnMouseMoveEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmousemove";
        }

        #endregion
    }
}