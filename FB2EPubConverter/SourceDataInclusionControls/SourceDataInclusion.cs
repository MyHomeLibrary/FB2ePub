using ConverterContracts.Settings;
using Fb2epubSettings;

namespace FB2EPubConverter.SourceDataInclusionControls
{
    /// <summary>
    /// FB2 Source info section inclusion control
    /// </summary>
    internal class SourceDataInclusion : IDataInclusionBase
    {
        public bool IsIgnoreInfoSource(IgnoreInfoSourceOptions ignoreInfoSourceOptions)
        {
            if (ignoreInfoSourceOptions == IgnoreInfoSourceOptions.IgnoreSourceTitle)
            {
                return true;
            }
            if (ignoreInfoSourceOptions == IgnoreInfoSourceOptions.IgnoreMainAndSource)
            {
                return true;
            }
            if (ignoreInfoSourceOptions != IgnoreInfoSourceOptions.IgnoreSourceAndPublish)
            {
                return true;
            }

            return false;
        }
    }
}
