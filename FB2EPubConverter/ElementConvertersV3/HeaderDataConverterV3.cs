using ConverterContracts.Settings;
using EPubLibrary;
using Fb2epubSettings;
using FB2Library;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class HeaderDataConverterV3
    {
        private readonly TitleInfoConverterV3 _titleInfoConverter;
        private readonly BookIDConverterV3 _bookIDConverter = new BookIDConverterV3();
        private readonly SourceInfoConverterV3 _sourceInfoConverter;
        private readonly PublisherInfoConverterV3 _publisherInfoConverter;
        private readonly SeriesDataConverterV3 _seriesDataConverter = new SeriesDataConverterV3();

        internal HeaderDataConverterV3(IEPubCommonSettings commonSettings, IEPubV3Settings v3Settings)
        {
            _titleInfoConverter = new TitleInfoConverterV3(commonSettings);
            _sourceInfoConverter = new SourceInfoConverterV3(commonSettings);
            _publisherInfoConverter = new PublisherInfoConverterV3(commonSettings);
        }

        public void Convert(EPubFileV3 epubFile, FB2File fb2File)
        {
            _titleInfoConverter.Convert(fb2File.TitleInfo,epubFile);
            _bookIDConverter.Convert(fb2File, epubFile);
            _sourceInfoConverter.Convert(fb2File, epubFile);
            _publisherInfoConverter.Convert(epubFile,fb2File);
            _seriesDataConverter.Convert(fb2File, epubFile);
        }
    }
}
