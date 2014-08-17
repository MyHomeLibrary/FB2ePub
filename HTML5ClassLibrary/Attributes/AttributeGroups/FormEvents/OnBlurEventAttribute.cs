using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// The onblur attribute fires the moment that the element loses focus.
    /// Onblur is most often used with form validation code (e.g. when the user leaves a form field).
    /// Tip: The onblur attribute is the opposite of the onfocus attribute.
    /// </summary>
    public class OnBlurEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onblur";
        }

        #endregion
    }
}