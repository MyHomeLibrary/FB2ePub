using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents
{
    /// <summary>
    /// The onkeyup attribute fires when the user releases a key (on the keyboard).
    ///Tip: The order of events related to the onkeyup event:
    ///    onkeydown
    ///    onkeypress
    ///    onkeyup
    ///Note: The onkeyup attribute CANNOT be used with: "base", "bdo", "br", "head", "html", "iframe", "meta", "param", "script", "style", or "title".
    /// </summary>
    public class OnKeyUpEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onkeyup";
        }

        #endregion
    }
}