using Fb2epubSettings;

namespace FB2EPubConverter.SourceDataInclusionControls
{
    /// <summary>
    /// Publish section inclusion control
    /// </summary>
    internal class PublishDataInclusion : IDataInclusionBase
    {
        public bool IsIgnoreInfoSource(IgnoreInfoSourceOptions ignoreInfoSourceOptions)
        {
            if (ignoreInfoSourceOptions == IgnoreInfoSourceOptions.IgnorePublishTitle)
            {
                return true;
            }
            if (ignoreInfoSourceOptions == IgnoreInfoSourceOptions.IgnoreSourceAndPublish)
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
