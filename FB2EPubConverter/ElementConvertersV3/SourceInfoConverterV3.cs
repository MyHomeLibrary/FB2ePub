using ConverterContracts.Settings;
using EPubLibraryContracts;
using FB2EPubConverter.SourceDataInclusionControls;
using FB2Library;
using TranslitRu;
using XHTMLClassLibrary.BaseElements.InlineElements.TextBasedElements;
using XHTMLClassLibrary.BaseElements.Structure_Header;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class SourceInfoConverterV3
    {
        private readonly IEPubConversionSettings _conversionSettings;

        internal SourceInfoConverterV3(IEPubConversionSettings conversionSettings)
        {
            _conversionSettings = conversionSettings;
        }

        public void Convert(FB2File fb2File, IBookInformationData titleInformation)
        {
            if ((fb2File.DocumentInfo.SourceOCR != null) && !string.IsNullOrEmpty(fb2File.DocumentInfo.SourceOCR.Text))
            {
                titleInformation.Source = new Source { SourceData = fb2File.DocumentInfo.SourceOCR.Text };
            }

            foreach (var docAuthor in fb2File.DocumentInfo.DocumentAuthors)
            {
                var person = new PersoneWithRole
                {
                    PersonName =
                        Rus2Lat.Instance.Translate(DescriptionConverters.GenerateAuthorString(docAuthor, _conversionSettings), _conversionSettings.TransliterationSettings),
                    FileAs = DescriptionConverters.GenerateFileAsString(docAuthor, _conversionSettings),
                    Role = RolesEnum.Adapter
                };
                if (fb2File.TitleInfo != null)
                {
                    person.Language = fb2File.TitleInfo.Language;
                }
                titleInformation.Contributors.Add(person);
            }

            // Getting information from FB2 Source Title Info section
            if (!SourceDataInclusionControl.Instance.IsIgnoreInfoSource(SourceDataInclusionControl.DataTypes.Source, _conversionSettings.IgnoreTitle) &&
                (fb2File.SourceTitleInfo.BookTitle != null) && 
                !string.IsNullOrEmpty(fb2File.SourceTitleInfo.BookTitle.Text))
            {
                var bookTitle = new Title
                {
                    TitleName =
                        Rus2Lat.Instance.Translate(fb2File.SourceTitleInfo.BookTitle.Text,
                            _conversionSettings.TransliterationSettings),
                    Language =
                        string.IsNullOrEmpty(fb2File.SourceTitleInfo.BookTitle.Language) &&
                        (fb2File.TitleInfo != null)
                            ? fb2File.TitleInfo.Language
                            : fb2File.SourceTitleInfo.BookTitle.Language,
                    TitleType = TitleType.SourceInfo
                };
                titleInformation.BookTitles.Add(bookTitle);
                titleInformation.Languages.Add(fb2File.SourceTitleInfo.Language);
            }

            // add authors
            foreach (var author in fb2File.SourceTitleInfo.BookAuthors)
            {
                var person = new PersoneWithRole
                {
                    PersonName =
                        Rus2Lat.Instance.Translate(
                            string.Format("{0} {1} {2}", author.FirstName, author.MiddleName, author.LastName),
                            _conversionSettings.TransliterationSettings),
                    FileAs = DescriptionConverters.GenerateFileAsString(author, _conversionSettings),
                    Role = RolesEnum.Author,
                    Language = fb2File.SourceTitleInfo.Language
                };
                titleInformation.Creators.Add(person);
            }

            TranslatorsInfoConverterV3.Convert(fb2File.SourceTitleInfo, titleInformation, _conversionSettings);
            GenresInfoConverterV3.Convert(fb2File.SourceTitleInfo, titleInformation,_conversionSettings);
        }

    }
}
