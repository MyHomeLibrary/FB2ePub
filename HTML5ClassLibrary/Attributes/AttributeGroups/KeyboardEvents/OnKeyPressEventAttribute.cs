using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents
{
    /// <summary>
    /// The onkeypress attribute fires when the user presses a key (on the keyboard).
    /// Tip: The order of events related to the onkeypress event:
    ///    onkeydown
    ///    onkeypress
    ///    onkeyup
    /// Note: The onkeypress event is not fired for all keys (e.g. ALT, CTRL, SHIFT, ESC) in all browsers. To detect only whether the user has pressed a key, use onkeydown instead, because it works for all keys.
    /// Note: The onkeypress attribute CANNOT be used with: "base", "bdo", "br", "head", "html", "iframe", "meta", "param", "script", "style", or "title".
    /// </summary>
    public class OnKeyPressEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onkeypress";
        }

        #endregion
    }
}