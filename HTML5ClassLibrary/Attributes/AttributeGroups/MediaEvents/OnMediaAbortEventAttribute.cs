using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run on abort
    /// </summary>
    public class OnMediaAbortEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onabort";
        }

        #endregion
    }
}
