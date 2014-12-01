using Fb2epubSettings;

namespace FB2EPubConverter.SourceDataInclusionControls
{
    /// <summary>
    /// Base interface for all inclusion control, so they can be used in same dictionary
    /// </summary>
    interface IDataInclusionBase
    {
        bool IsIgnoreInfoSource(IgnoreInfoSourceOptions ignoreInfoSourceOptions);
    }
}
