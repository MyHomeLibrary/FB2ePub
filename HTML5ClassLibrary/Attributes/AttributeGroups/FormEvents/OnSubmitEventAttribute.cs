using HTMLClassLibrary.Attributes.Events;

namespace HTMLClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// The onsubmit attribute fires when a form is submitted.
    /// The onsubmit attribute is only used within: <form>.
    /// </summary>
    public class OnSubmitEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onsubmit";
        }

        #endregion

    }
}
