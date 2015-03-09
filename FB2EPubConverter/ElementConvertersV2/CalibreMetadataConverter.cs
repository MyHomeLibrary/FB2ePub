using EPubLibraryContracts;
using EPubLibraryContracts.Settings;
using FB2Library;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class CalibreMetadataConverter
    {
        private readonly IEPubV2Settings _v2Settings;

        public CalibreMetadataConverter(IEPubV2Settings v2Settings)
        {
            _v2Settings = v2Settings;
        }


        internal void Convert(FB2File fb2File, ICalibreMetadata metadata)
        {
            if (!_v2Settings.AddCalibreMetadata)
            {
                return;
            }
            if (fb2File.TitleInfo != null && fb2File.TitleInfo.BookTitle != null &&
                !string.IsNullOrEmpty(fb2File.TitleInfo.BookTitle.Text))
            {
                metadata.TitleForSort = fb2File.TitleInfo.BookTitle.Text;
            }
            if (fb2File.TitleInfo != null && fb2File.TitleInfo.Sequences.Count > 0 &&
                !string.IsNullOrEmpty(fb2File.TitleInfo.Sequences[0].Name))
            {
                metadata.SeriesName = fb2File.TitleInfo.Sequences[0].Name;
                if (fb2File.TitleInfo.Sequences[0].Number.HasValue)
                {
                    metadata.SeriesIndex = fb2File.TitleInfo.Sequences[0].Number.Value;
                }
            }
            
        }
    }
}
