using ConverterContracts.Settings;
using EPubLibraryContracts;
using FB2Library.HeaderItems;
using TranslitRu;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal static class TranslatorsInfoConverterV2
    {
        public static void Convert(ItemTitleInfo titleInfo, IBookInformationData titleInformation, IEPubConversionSettings settings)
        {
            foreach (var translator in titleInfo.Translators)
            {
                var person = new PersoneWithRole
                {
                    PersonName = Rus2Lat.Instance.Translate(DescriptionConverters.GenerateAuthorString(translator, settings),
                        settings.TransliterationSettings),
                    FileAs = DescriptionConverters.GenerateFileAsString(translator, settings),
                    Role = RolesEnum.Translator,
                    Language = titleInfo.Language
                };
               titleInformation.Contributors.Add(person);
            }
        }

    }
}
