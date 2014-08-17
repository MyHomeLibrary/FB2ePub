using XHTMLClassLibrary.Attributes.Events;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// The onfocus attribute fires the moment that the element gets focus.
    /// Onfocus is most often used with "inpu">, "select", and "a".
    /// Tip: The onfocus attribute is the opposite of the onblur attribute.
    /// Note: The onfocus attribute CANNOT be used with: "base", "bdo", "br", "head", "html", "iframe", "meta", "param", "script", "style", or "title".
    /// </summary>
    public class OnFocusEventAttribute : OnEventAttribute
    {
        #region Overrides of OnEventAttribute

        protected override string GetAttributeName()
        {
            return "onfocus";
        }

        #endregion
    }
}