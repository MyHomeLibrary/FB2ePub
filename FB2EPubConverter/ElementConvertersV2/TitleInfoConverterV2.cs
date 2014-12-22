using System;
using EPubLibrary;
using EPubLibrary.XHTML_Items;
using FB2EPubConverter.SourceDataInclusionControls;
using Fb2epubSettings;
using FB2Library.HeaderItems;
using TranslitRu;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class TitleInfoConverterV2
    {
        private readonly EPubCommonSettings _commonSettings;

        public TitleInfoConverterV2(EPubCommonSettings commonSettings)
        {
            _commonSettings = commonSettings;
        }

        public void Convert(ItemTitleInfo itemTitleInfo, EPubFileV2 epubFile)
        {
            epubFile.Title.Languages.Clear();
            epubFile.Title.Creators.Clear();
            epubFile.Title.Contributors.Clear();
            epubFile.Title.Subjects.Clear();
            epubFile.Title.Identifiers.Clear();

            // Create new Title page
            epubFile.TitlePage = new TitlePageFile(HTMLElementType.XHTML11);

            // in case main body title is not defined (empty) 
            if ((itemTitleInfo != null) && (itemTitleInfo.BookTitle != null))
            {
                epubFile.TitlePage.BookTitle = itemTitleInfo.BookTitle.Text;
            }

            epubFile.AllSequences.Clear();

            if (itemTitleInfo != null)
            {
                SequencesInfoConverterV2.Convert(itemTitleInfo, epubFile);

                // Getting information from FB2 Title section
                ConvertMainTitle(itemTitleInfo, epubFile);

                // add authors
                AuthorsInfoConverterV2.Convert(itemTitleInfo, epubFile, _commonSettings);

                // add translators
                TranslatorsInfoConverterV2.Convert(itemTitleInfo, epubFile,_commonSettings);

                // genres
                GenresInfoConverterV2.Convert(itemTitleInfo, epubFile);

            }
            epubFile.Title.DateFileCreation = DateTime.Now;
        }


        private void ConvertMainTitle(ItemTitleInfo titleInfo, EPubFileV2 epubFile)
        {
            if (!SourceDataInclusionControl.Instance.IsIgnoreInfoSource(SourceDataInclusionControl.DataTypes.Title, _commonSettings.IgnoreTitle))
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
}
