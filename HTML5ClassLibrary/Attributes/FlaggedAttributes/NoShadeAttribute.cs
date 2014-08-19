using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class NoShadeAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "noshade";

        public override string GetElementName()
        {
            return AttributeName;
        }

    }
}
