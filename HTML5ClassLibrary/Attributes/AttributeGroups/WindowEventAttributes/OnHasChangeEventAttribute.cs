using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// Script to be run when the document has changed
    /// </summary>
    public class OnHasChangeEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onhaschange";
        }

        #endregion
    }
}
