using EPubLibrary;
using FB2Library.HeaderItems;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal static class GenresInfoConverterV2
    {
        public static void Convert(ItemTitleInfo titleInfo, EPubFileV2 epubFile)
        {
            foreach (var genre in titleInfo.Genres)
            {
                var item = new Subject
                {
                    SubjectInfo = epubFile.Transliterator.Translate(DescriptionConverters.Fb2GenreToDescription(genre.Genre),
                        epubFile.TranslitMode)
                };
                epubFile.Title.Subjects.Add(item);
            }
        }

    }
}
