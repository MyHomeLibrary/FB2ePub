using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// The onchange attribute fires the moment when the value of the element is changed.
    /// The onchange attribute can be used with the "input", "textarea", and "select" elements.
    /// </summary>
    public class OnChangeEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onchange";
        }

        #endregion

    }
}