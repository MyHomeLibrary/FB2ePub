
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// The onmousedown attribute fires when a mouse button is pressed down on the element.
    /// Tip: The order of events related to the onmousedown event (for the left/middle mouse button):
    ///    onmousedown
    ///    onmouseup
    ///    onclick
    // The order of events related to the onmousedown event (for the right mouse button):
    ///    onmousedown
    ///    onmouseup
    ///    oncontextmenu
    /// Note: The onmousedown attribute CANNOT be used with: "base", "bdo", "br", "head", "html", "iframe", "meta", "param", "script", "style", or "title".
    /// </summary>
    public class OnMouseDownEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmousedown";
        }

        #endregion
    }
}