using ConverterContracts.Settings;
using EPubLibraryContracts;
using EPubLibraryContracts.Settings;
using FB2Library.HeaderItems;
using TranslitRu;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal static class GenresInfoConverterV3
    {
        public static void Convert(ItemTitleInfo titleInfo, IBookInformationData titleInformation,IEPubConversionSettings settings)
        {
            foreach (var genre in titleInfo.Genres)
            {
                var item = new Subject
                {
                    SubjectInfo = Rus2Lat.Instance.Translate(DescriptionConverters.Fb2GenreToDescription(genre.Genre),
                        settings.TransliterationSettings)
                };
               titleInformation.Subjects.Add(item);
            }
        }

    }
}
