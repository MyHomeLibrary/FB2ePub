using System;
using ConverterContracts.Settings;
using EPubLibrary;
using EPubLibrary.XHTML_Items;
using EPubLibraryContracts;
using FB2Library.HeaderItems;
using TranslitRu;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class TitleInfoConverterV3
    {
        private readonly IEPubConversionSettings _commonSettings;

        public TitleInfoConverterV3(IEPubConversionSettings commonSettings)
        {
            _commonSettings = commonSettings;
        }

        public void Convert(ItemTitleInfo itemTitleInfo, EPubFileV3 epubFile)
        {
            epubFile.Title.Languages.Clear();
            epubFile.Title.Creators.Clear();
            epubFile.Title.Contributors.Clear();
            epubFile.Title.Subjects.Clear();
            epubFile.Title.Identifiers.Clear();


            // in case main body title is not defined (empty) 
            if ((itemTitleInfo != null) && (itemTitleInfo.BookTitle != null))
            {
                epubFile.BookInformation.BookMainTitle = itemTitleInfo.BookTitle.Text;
            }

            epubFile.AllSequences.Clear();

            if (itemTitleInfo != null)
            {
                // Pass all sequences
                SequencesInfoConverterV3.Convert(itemTitleInfo, epubFile.BookInformation);

                // Getting information from FB2 Title section
                ConvertMainTitle(itemTitleInfo, epubFile);

                // add authors
                AuthorsInfoConverterV3.Convert(itemTitleInfo, _commonSettings, epubFile.BookInformation);

                // add translators
                TranslatorsInfoConverterV3.Convert(itemTitleInfo, epubFile,_commonSettings);

                // genres
                GenresInfoConverterV3.Convert(itemTitleInfo, epubFile);
            }
            epubFile.Title.DateFileCreation = DateTime.Now;
        }

        private void ConvertMainTitle(ItemTitleInfo titleInfo, EPubFileV3 epubFile)
        {
            var bookTitle = new Title
            {
                TitleName = Rus2Lat.Instance.Translate(DescriptionConverters.FormatBookTitle(titleInfo, _commonSettings),
                    epubFile.TranslitMode),
                Language = string.IsNullOrEmpty(titleInfo.BookTitle.Language)
                    ? titleInfo.Language
                    : titleInfo.BookTitle.Language,
                TitleType = TitleType.Main
            };
            epubFile.Title.BookTitles.Add(bookTitle);
            // add main title language
            epubFile.Title.Languages.Add(titleInfo.Language);
        }
    }
}
