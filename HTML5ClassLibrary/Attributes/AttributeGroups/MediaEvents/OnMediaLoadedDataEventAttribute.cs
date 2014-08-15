using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run when media data is loaded
    /// </summary>
    public class OnMediaLoadedDataEventAttribute: OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onloadeddata";
        }

        #endregion
    }
}
