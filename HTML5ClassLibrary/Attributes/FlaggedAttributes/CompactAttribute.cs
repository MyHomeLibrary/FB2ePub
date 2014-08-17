using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHTMLClassLibrary.Attributes.FlaggedAttributes
{
    /// <summary>
    /// The compact attribute is a boolean attribute.
    /// When present, it specifies that the list should render smaller than normal, by reducing the space between lines and the indentation of the list.
    /// </summary>
    public class CompactAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "compact";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}
