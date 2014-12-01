using System.Collections.Generic;
using Fb2epubSettings;

namespace FB2EPubConverter.SourceDataInclusionControls
{
    /// <summary>
    /// This class controls if speciffic FB2 section information need to be included based on settings
    /// </summary>
    internal class SourceDataInclusionControl 
    {
        public enum DataTypes
        {
            Title,
            Source,
            Publish,
        }

        private readonly Dictionary<DataTypes,IDataInclusionBase>  _inclusionControls = new Dictionary<DataTypes, IDataInclusionBase>();

        public SourceDataInclusionControl()
        {
            _inclusionControls.Add(DataTypes.Title, new TitleDataInclusion());
            _inclusionControls.Add(DataTypes.Source,new SourceDataInclusion());
            _inclusionControls.Add(DataTypes.Publish,new PublishDataInclusion());
        }

        public bool IsIgnoreInfoSource(DataTypes type,IgnoreInfoSourceOptions ignoreInfoSourceOptions)
        {
            return _inclusionControls[type].IsIgnoreInfoSource(ignoreInfoSourceOptions);
        }
    }
}
