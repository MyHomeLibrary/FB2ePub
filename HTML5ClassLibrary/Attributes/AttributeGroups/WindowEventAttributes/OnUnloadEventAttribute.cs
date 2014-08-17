using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// The onunload attribute fires once a page has unloaded (or the browser window has been closed).
    /// onunload occurs when the user navigates away from the page (by clicking on a link, submitting a form, closing the browser window, etc.)
    /// Note: If you reload a page, you will also trigger the unload event (and the onload event).
    /// </summary>
    public class OnUnloadEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onunload";
        }

        #endregion
    }
}
