using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPubLibrary
{
    static class V3StandardChecker
    {
        /// <summary>
        /// Check if collection allowed by standard implementation
        /// </summary>
        /// <param name="standard"></param>
        /// <returns></returns>
        public static bool IsCollectionsAllowedByStandard(V3Standard standard)
        {
            if (standard == V3Standard.V30)
            {
                return false;
            }
            return true;
        }

        internal static bool IsRenditionFlowAllowedByStandard(V3Standard standard)
        {
            if (standard == V3Standard.V30)
            {
                return false;
            }
            return true;
        }
    }
}
