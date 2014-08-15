
using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// The onselect attribute fires after some text has been selected in an element.
    /// The onselect attribute can be used within: 'input type="file"', 'input type="password"', 'input type="text"', 'keygen', and 'textarea'.
    ///</summary>
    public class OnSelectEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onselect";
        }

        #endregion

    }
}