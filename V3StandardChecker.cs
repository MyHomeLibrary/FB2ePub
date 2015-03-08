using EPubLibraryContracts.Settings;

namespace EPubLibrary
{
    static class V3StandardChecker
    {
        /// <summary>
        /// Check if collection allowed by standard implementation
        /// </summary>
        /// <param name="standard"></param>
        /// <returns></returns>
        public static bool IsCollectionsAllowedByStandard(EPubV3SubStandard standard)
        {
            if (standard == EPubV3SubStandard.V30)
            {
                return false;
            }
            return true;
        }

        internal static bool IsRenditionFlowAllowedByStandard(EPubV3SubStandard standard)
        {
            if (standard == EPubV3SubStandard.V30)
            {
                return false;
            }
            return true;
        }
    }
}
