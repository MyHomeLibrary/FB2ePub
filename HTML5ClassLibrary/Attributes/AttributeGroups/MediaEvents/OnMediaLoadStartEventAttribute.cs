using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.MediaEvents
{
    /// <summary>
    /// Script to be run just as the file begins to load before anything is actually loaded
    /// </summary>
    public class OnMediaLoadStartEventAttribute: OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onloadstart";
        }

        #endregion
    }
}
