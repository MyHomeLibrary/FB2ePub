using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPubLibrary;
using FB2EPubConverter.SourceDataInclusionControls;
using Fb2epubSettings;
using FB2Library;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class SourceInfoConverterV3
    {
        private readonly EPubCommonSettings _commonSettings;

        internal SourceInfoConverterV3(EPubCommonSettings commonSettings)
        {
            _commonSettings = commonSettings;
        }

        public void Convert(FB2File fb2File, EPubFileV3 epubFile)
        {
            if ((fb2File.DocumentInfo.SourceOCR != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.SourceOCR.Text))
            {
                epubFile.Title.Source = new Source { SourceData = fb2File.DocumentInfo.SourceOCR.Text };
            }

            foreach (var docAuthor in fb2File.DocumentInfo.DocumentAuthors)
            {
                var person = new PersoneWithRole
                {
                    PersonName =
                        epubFile.Transliterator.Translate(DescriptionConverters.GenerateAuthorString(docAuthor, _commonSettings), epubFile.TranslitMode),
                    FileAs = DescriptionConverters.GenerateFileAsString(docAuthor, _commonSettings),
                    Role = RolesEnum.Adapter
                };
                if (fb2File.TitleInfo != null)
                {
                    person.Language = fb2File.TitleInfo.Language;
                }
                epubFile.Title.Contributors.Add(person);
            }

            // Getting information from FB2 Source Title Info section
            if (!SourceDataInclusionControl.Instance.IsIgnoreInfoSource(SourceDataInclusionControl.DataTypes.Source, _commonSettings.IgnoreTitle) &&
                (fb2File.SourceTitleInfo.BookTitle != null) && 
                !string.IsNullOrEmpty(fb2File.SourceTitleInfo.BookTitle.Text))
            {
                var bookTitle = new Title
                {
                    TitleName =
                        epubFile.Transliterator.Translate(fb2File.SourceTitleInfo.BookTitle.Text,
                            epubFile.TranslitMode),
                    Language =
                        string.IsNullOrEmpty(fb2File.SourceTitleInfo.BookTitle.Language) &&
                        (fb2File.TitleInfo != null)
                            ? fb2File.TitleInfo.Language
                            : fb2File.SourceTitleInfo.BookTitle.Language,
                    TitleType = TitleType.SourceInfo
                };
                epubFile.Title.BookTitles.Add(bookTitle);
                epubFile.Title.Languages.Add(fb2File.SourceTitleInfo.Language);
            }

            // add authors
            foreach (var author in fb2File.SourceTitleInfo.BookAuthors)
            {
                var person = new PersoneWithRole
                {
                    PersonName =
                        epubFile.Transliterator.Translate(
                            string.Format("{0} {1} {2}", author.FirstName, author.MiddleName, author.LastName),
                            epubFile.TranslitMode),
                    FileAs = DescriptionConverters.GenerateFileAsString(author, _commonSettings),
                    Role = RolesEnum.Author,
                    Language = fb2File.SourceTitleInfo.Language
                };
                epubFile.Title.Creators.Add(person);
            }

            TranslatorsInfoConverterV3.Convert(fb2File.SourceTitleInfo, epubFile, _commonSettings);
            GenresInfoConverterV3.Convert(fb2File.SourceTitleInfo, epubFile);
        }

    }
}
