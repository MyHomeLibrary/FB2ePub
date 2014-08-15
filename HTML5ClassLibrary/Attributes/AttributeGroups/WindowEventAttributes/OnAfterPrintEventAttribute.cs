using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// The onafterprint attribute fires after the user has set the page to print, and the print dialogue box has appeared.
    /// Tip: The onafterprint attribute is often used together with the onbeforeprint attribute.
    /// </summary>
    public class OnAfterPrintEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onafterprint";
        }

        #endregion

    }
}
