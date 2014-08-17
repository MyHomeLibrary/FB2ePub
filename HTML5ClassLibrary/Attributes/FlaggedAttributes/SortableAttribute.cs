using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class SortableAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "sortable";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}
