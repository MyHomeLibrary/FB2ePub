using ConverterContracts.Settings;

namespace FB2EPubConverter.SourceDataInclusionControls
{
    /// <summary>
    /// FB2 Title section inclusion control
    /// </summary>
    internal class TitleDataInclusion : IDataInclusionBase
    {
        public bool IsIgnoreInfoSource(IgnoreInfoSourceOptions ignoreInfoSourceOptions)
        {
            if (ignoreInfoSourceOptions == IgnoreInfoSourceOptions.IgnoreMainTitle)
            {
                return true;
            }
            if (ignoreInfoSourceOptions == IgnoreInfoSourceOptions.IgnoreMainAndSource)
            {
                return true;
            }
            if (ignoreInfoSourceOptions == IgnoreInfoSourceOptions.IgnoreMainAndPublish)
            {
                return true;
            }
            return false;
        }
    }
}
