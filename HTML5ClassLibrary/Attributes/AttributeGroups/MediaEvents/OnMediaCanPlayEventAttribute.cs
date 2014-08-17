using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when a file is ready to start playing (when it has buffered enough to begin)
    /// </summary>
    public class OnMediaCanPlayEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "oncanplay";
        }

        #endregion
    }
}
