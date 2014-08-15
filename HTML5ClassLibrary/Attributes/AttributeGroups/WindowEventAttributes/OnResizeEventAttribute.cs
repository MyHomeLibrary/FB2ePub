using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
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
