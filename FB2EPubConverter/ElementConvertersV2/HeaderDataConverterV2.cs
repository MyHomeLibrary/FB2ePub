using EPubLibrary;
using Fb2epubSettings;
using FB2Library;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class HeaderDataConverterV2
    {
        private readonly TitleInfoConverterV2 _titleInfoConverter;
        private readonly SourceInfoConverterV2 _sourceInfoConverter;
        private readonly PublisherInfoConverterV2 _publisherInfoConverter;
        private readonly CalibreMetadataConverter _calibreMetadataConverter;
        private readonly SeriesDataConverterV2 _seriesDataConverter = new SeriesDataConverterV2();
        private readonly BookIDConverterV2 _bookIDConverter = new BookIDConverterV2();

        public HeaderDataConverterV2(EPubCommonSettings commonSettings,EPubV2Settings v2Settings)
        {
            _titleInfoConverter = new TitleInfoConverterV2(commonSettings);
            _sourceInfoConverter = new SourceInfoConverterV2(commonSettings);
            _publisherInfoConverter = new PublisherInfoConverterV2(commonSettings);
            _calibreMetadataConverter   =   new CalibreMetadataConverter(v2Settings);
        }

        public void Convert(EPubFile epubFile, FB2File fb2File)
        {
            _titleInfoConverter.Convert(fb2File.TitleInfo, epubFile);
            _bookIDConverter.Convert(fb2File, epubFile);
            _sourceInfoConverter.Convert(fb2File, epubFile);
            _publisherInfoConverter.Convert(fb2File, epubFile);
            _calibreMetadataConverter.Convert(fb2File, epubFile);
            _seriesDataConverter.Convert(fb2File, epubFile);
        }
    }
}
