using System;
using FB2EPubConverter.SourceDataInclusionControls;
using FB2Library;
using TranslitRu;
using ConverterContracts.Settings;
using EPubLibraryContracts;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class PublisherInfoConverterV2
    {
        private readonly IEPubConversionSettings _conversionSettings;

        public PublisherInfoConverterV2(IEPubConversionSettings conversionSettings)
        {
            _conversionSettings = conversionSettings;
        }

        public void Convert(FB2File fb2File, IBookInformationData titleInformation)
        {
            if (fb2File.PublishInfo.BookTitle != null)
            {
                var bookTitle = new Title
                {
                    TitleName =
                        Rus2Lat.Instance.Translate(fb2File.PublishInfo.BookTitle.Text, _conversionSettings.TransliterationSettings),
                    Language =
                        !string.IsNullOrEmpty(fb2File.PublishInfo.BookTitle.Language)
                            ? fb2File.PublishInfo.BookTitle.Language
                            : fb2File.TitleInfo.Language
                };
                if (!SourceDataInclusionControl.Instance.IsIgnoreInfoSource(SourceDataInclusionControl.DataTypes.Publish, _conversionSettings.IgnoreTitle))
                {
                    bookTitle.TitleType = TitleType.PublishInfo;
                    titleInformation.BookTitles.Add(bookTitle);
                }
            }


            if (fb2File.PublishInfo.ISBN != null)
            {
                var bookId = new Identifier
                {
                    IdentifierName = "BookISBN",
                    ID = fb2File.PublishInfo.ISBN.Text,
                    Scheme = "ISBN"
                };
               titleInformation.Identifiers.Add(bookId);
            }

            titleInformation.Publisher = new Publisher();
            if (fb2File.PublishInfo.Publisher != null)
            {
               titleInformation.Publisher.PublisherName = Rus2Lat.Instance.Translate(fb2File.PublishInfo.Publisher.Text, _conversionSettings.TransliterationSettings);
            }


            try
            {
                if (fb2File.PublishInfo.Year.HasValue)
                {
                    var date = new DateTime(fb2File.PublishInfo.Year.Value, 1, 1);
                    titleInformation.DatePublished = date;
                }
            }
            catch (FormatException ex)
            {
                Logger.Log.DebugFormat("Date reading format exception: {0}", ex);
            }
            catch (Exception exAll)
            {
                Logger.Log.ErrorFormat("Date reading exception: {0}", exAll);
            }
        }   
       
    }
}
