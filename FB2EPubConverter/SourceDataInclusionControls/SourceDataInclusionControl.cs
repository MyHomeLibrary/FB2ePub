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
        private readonly static SourceDataInclusionControl SingleTone;

        private  SourceDataInclusionControl()
        {
            _inclusionControls.Add(DataTypes.Title, new TitleDataInclusion());
            _inclusionControls.Add(DataTypes.Source,new SourceDataInclusion());
            _inclusionControls.Add(DataTypes.Publish,new PublishDataInclusion());
        }

        static SourceDataInclusionControl()
        {
            if (SingleTone == null)
            {
                SingleTone = new SourceDataInclusionControl();
            }
        }

        public static SourceDataInclusionControl Instance
        {
            get { return SingleTone; }
        }

        public bool IsIgnoreInfoSource(DataTypes type,IgnoreInfoSourceOptions ignoreInfoSourceOptions)
        {
            return _inclusionControls[type].IsIgnoreInfoSource(ignoreInfoSourceOptions);
        }
    }
}
