using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    public class BDI : TextBasedElement
    {
        internal const string ElementName = "bdi";

        #region Overrides of TextBasedElement

        protected override string GetElementName()
        {
            return ElementName;
        }

        #endregion
    }
}
