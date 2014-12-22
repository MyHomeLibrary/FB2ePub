using EPubLibrary;
using FB2Library.HeaderItems;
using TranslitRu;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal static class GenresInfoConverterV3
    {
        public static void Convert(ItemTitleInfo titleInfo, EPubFileV3 epubFile)
        {
            foreach (var genre in titleInfo.Genres)
            {
                var item = new Subject
                {
                    SubjectInfo = Rus2Lat.Instance.Translate(DescriptionConverters.Fb2GenreToDescription(genre.Genre),
                        epubFile.TranslitMode)
                };
                epubFile.Title.Subjects.Add(item);
            }
        }

    }
}
