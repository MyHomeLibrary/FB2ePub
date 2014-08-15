using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.WindowEventAttributes
{
    /// <summary>
    /// Script to be run when a Web Storage area is updated
    /// </summary>
    public class OnStorageEventAttribute: OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onstorage";
        }

        #endregion
    }
}
