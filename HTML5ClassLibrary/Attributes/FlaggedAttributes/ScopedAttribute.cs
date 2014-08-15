using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class ScopedAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "scoped";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}
