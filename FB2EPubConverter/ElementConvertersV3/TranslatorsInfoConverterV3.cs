using ConverterContracts.Settings;
using EPubLibrary;
using FB2Library.HeaderItems;
using TranslitRu;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal static class TranslatorsInfoConverterV3
    {
        public static void Convert(ItemTitleInfo titleInfo, EPubFileV3 epubFile, IEPubConversionSettings settings)
        {
            foreach (var translator in titleInfo.Translators)
            {
                var person = new PersoneWithRole
                {
                    PersonName = Rus2Lat.Instance.Translate(DescriptionConverters.GenerateAuthorString(translator, settings),
                        epubFile.TranslitMode),
                    FileAs = DescriptionConverters.GenerateFileAsString(translator, settings),
                    Role = RolesEnum.Translator,
                    Language = titleInfo.Language
                };
                epubFile.Title.Contributors.Add(person);
            }
        }

    }
}
