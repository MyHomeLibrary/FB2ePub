using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// The onbeforeprint attribute fires immediately after the user has set the page to print, but before the print dialogue box appears.
    /// Tip: The onbeforeprint attribute is often used together with the onafterprint attribute.
    /// </summary>
    public class OnBeforePrintEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onbeforeprint";
        }

        #endregion

    }
}
