using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLClassLibrary.Attributes.FlaggedAttributes
{
    public class ChallengeAttribute : BaseFlagAttribute
    {
        private const string AttributeName = "challenge";

        #region Overrides of BaseFlagAttribute

        public override string GetElementName()
        {
            return AttributeName;
        }

        #endregion
    }
}
