using ConverterContracts.Settings;
using EPubLibrary;
using EPubLibraryContracts;
using FB2Library.HeaderItems;
using TranslitRu;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal static class AuthorsInfoConverterV3
    {
        public static void Convert(ItemTitleInfo titleInfo, EPubFileV3 epubFile, IEPubConversionSettings settings, IBookTitleInformation titleInformation)
        {
            foreach (var author in titleInfo.BookAuthors)
            {
                var person = new PersoneWithRole();
                string authorString = DescriptionConverters.GenerateAuthorString(author, settings);
                person.PersonName = Rus2Lat.Instance.Translate(authorString, epubFile.TranslitMode);
                person.FileAs = DescriptionConverters.GenerateFileAsString(author, settings);
                person.Role = RolesEnum.Author;
                person.Language = titleInfo.Language;
                epubFile.Title.Creators.Add(person);

                // add authors to Title page
                titleInformation.Authors.Add(authorString);
            }
        }

    }
}
