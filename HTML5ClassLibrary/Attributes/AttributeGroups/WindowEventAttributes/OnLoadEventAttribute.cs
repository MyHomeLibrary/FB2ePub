using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// The onload attribute fires when an object has been loaded.
    /// onload is most often used within the "bod"> element to execute a script once a web page has completely loaded all content (including images, script files, CSS files, etc.).
    /// </summary>
    public class OnLoadEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onload";
        }

        #endregion
    }
}
