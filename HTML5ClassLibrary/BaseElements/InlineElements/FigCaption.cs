using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "figcaption" tag defines a caption for a "figure" element.
    /// The "figcaption" element can be placed as the first or last child of the "figure" element.
    /// </summary>
    public class FigCaption : TextBasedElement
    {
        public const string ElementName = "figcaption";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
