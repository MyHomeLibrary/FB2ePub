using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// Script to be run when the document performs a redo
    /// </summary>
    public class OnRedoEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onredo";
        }

        #endregion
    }
}
