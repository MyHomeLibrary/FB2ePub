using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibrary;
using FB2EPubConverter.SourceDataInclusionControls;
using Fb2epubSettings;
using FB2Library;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class PublisherInfoConverterV2
    {
        private readonly EPubCommonSettings _commonSettings;

        public PublisherInfoConverterV2(EPubCommonSettings commonSettings)
        {
            _commonSettings = commonSettings;
        }

        public void Convert(FB2File fb2File, EPubFileV2 epubFile)
        {
            if (fb2File.PublishInfo.BookTitle != null)
            {
                var bookTitle = new Title
                {
                    TitleName =
                        epubFile.Transliterator.Translate(fb2File.PublishInfo.BookTitle.Text, epubFile.TranslitMode),
                    Language =
                        !string.IsNullOrEmpty(fb2File.PublishInfo.BookTitle.Language)
                            ? fb2File.PublishInfo.BookTitle.Language
                            : fb2File.TitleInfo.Language
                };
                if (!SourceDataInclusionControl.Instance.IsIgnoreInfoSource(SourceDataInclusionControl.DataTypes.Publish, _commonSettings.IgnoreTitle))
                {
                    bookTitle.TitleType = TitleType.PublishInfo;
                    epubFile.Title.BookTitles.Add(bookTitle);
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
                epubFile.Title.Identifiers.Add(bookId);
            }


            if (fb2File.PublishInfo.Publisher != null)
            {
                epubFile.Title.Publisher.PublisherName = epubFile.Transliterator.Translate(fb2File.PublishInfo.Publisher.Text, epubFile.TranslitMode);
            }


            try
            {
                if (fb2File.PublishInfo.Year.HasValue)
                {
                    var date = new DateTime(fb2File.PublishInfo.Year.Value, 1, 1);
                    epubFile.Title.DatePublished = date;
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
