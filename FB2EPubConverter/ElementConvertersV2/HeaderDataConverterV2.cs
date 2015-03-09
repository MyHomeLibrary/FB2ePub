using ConverterContracts.Settings;
using EPubLibraryContracts;
using EPubLibraryContracts.Settings;
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

        public HeaderDataConverterV2(IEPubConversionSettings commonSettings,IEPubV2Settings v2Settings)
        {
            _titleInfoConverter = new TitleInfoConverterV2(commonSettings);
            _sourceInfoConverter = new SourceInfoConverterV2(commonSettings);
            _publisherInfoConverter = new PublisherInfoConverterV2(commonSettings);
            _calibreMetadataConverter   =   new CalibreMetadataConverter(v2Settings);
        }

        public void Convert(FB2File fb2File,IBookInformationData titleInformation, ICalibreMetadata metadata)
        {
            _titleInfoConverter.Convert(fb2File.TitleInfo, titleInformation);
            _bookIDConverter.Convert(fb2File, titleInformation);
            _sourceInfoConverter.Convert(fb2File, titleInformation);
            _publisherInfoConverter.Convert(fb2File, titleInformation);
            _calibreMetadataConverter.Convert(fb2File, metadata);
            _seriesDataConverter.Convert(fb2File, titleInformation);
        }
    }
}
