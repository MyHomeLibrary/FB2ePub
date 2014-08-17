using XHTMLClassLibrary.Attributes.FlaggedAttributes;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// The hidden attribute is a boolean attribute.
    /// When present, it specifies that an element is not yet, or is no longer, relevant.
    /// Browsers should not display elements that have the hidden attribute specified.
    /// The hidden attribute can also be used to keep a user from seeing an element until some other condition has been met (like selecting a checkbox, etc.). Then, a JavaScript could remove the hidden attribute, and make the element visible.
    /// </summary>
    public class HiddenAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "hidden";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion

    }
}
