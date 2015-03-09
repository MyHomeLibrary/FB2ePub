using ConverterContracts.Settings;
using EPubLibraryContracts;
using FB2Library.HeaderItems;
using TranslitRu;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal static class GenresInfoConverterV2
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
