using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHTMLClassLibrary.Attributes.FlaggedAttributes
{
    /// <summary>
    /// The required attribute is a boolean attribute.
    /// When present, it specifies that an input field must be filled out before submitting the form.
    /// Note: The required attribute works with the following input types: text, search, url, tel, email, password, date pickers, number, checkbox, radio, and file.
    /// </summary>
    public class RequiredAttribute : BaseFlagAttribute
    {

        private const string AttributeName = "required";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}
