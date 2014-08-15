using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents
{
    /// <summary>
    /// The onkeydown attribute fires when the user is pressing a key (on the keyboard).
    /// Tip: The order of events related to the onkeydown event:
    ///    onkeydown
    ///    onkeypress
    ///    onkeyup
    /// Note: The onkeydown attribute CANNOT be used with: "base", "bdo", "br", "head", "html", "iframe", "meta", "param", "script", "style", or "title".
    /// </summary>
    public class OnKeyDownEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onkeydown";
        }

        #endregion
    }
}