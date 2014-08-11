using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// The onresize attribute fires when an object is resized.
    /// The onresize attribute is most often used when the browser window is resized.
    /// </summary>
    public class OnResizeEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onresize";
        }

        #endregion
    }
}
