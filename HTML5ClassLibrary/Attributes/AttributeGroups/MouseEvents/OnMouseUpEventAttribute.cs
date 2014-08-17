﻿
using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MouseEvents
{
    /// <summary>
    /// The onmouseup attribute fires when a mouse button is released over the element.
    /// Tip: The order of events related to the onmouseup event (for the left/middle mouse button):
    ///    onmousedown
    ///    onmouseup
    ///    onclick
    /// The order of events related to the onmouseup event (for the right mouse button):
    ///    onmousedown
    ///    onmouseup
    ///    oncontextmenu
    /// Note: The onmouseup attribute CANNOT be used with: "base", "bdo", "br", "head", "html", "iframe", "meta", "param", "script", "style", or "title".
    /// </summary>
    public class OnMouseUpEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onmouseup";
        }

        #endregion
    }
}