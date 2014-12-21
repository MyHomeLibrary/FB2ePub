using System.Linq;
using EPubLibrary;
using EPubLibrary.Content.Collections;
using FB2Library;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class SeriesDataConverterV3
    {
        public void Convert(FB2File fb2File, EPubFileV3 epubFile)
        {
            foreach (var seq in fb2File.TitleInfo.Sequences)
            {
                if (!string.IsNullOrEmpty(seq.Name))
                {
                    var collectionMember = new SeriesCollectionMember
                    {
                        CollectionName = seq.Name,
                        Type = SeriesCollectionMember.CollectionType.Series,
                        CollectionPosition = seq.Number
                    };
                    epubFile.AddNewCollection(collectionMember);
                    foreach (var subseq in seq.SubSections.Where(subseq => !string.IsNullOrEmpty(subseq.Name)))
                    {
                        collectionMember = new SeriesCollectionMember
                        {
                            CollectionName = subseq.Name,
                            Type = SeriesCollectionMember.CollectionType.Set,
                            CollectionPosition = subseq.Number
                        };
                        epubFile.AddNewCollection(collectionMember);
                    }
                }
            }
            foreach (var seq in fb2File.SourceTitleInfo.Sequences)
            {
                if (!string.IsNullOrEmpty(seq.Name))
                {
                    var collectionMember = new SeriesCollectionMember
                    {
                        CollectionName = seq.Name,
                        Type = SeriesCollectionMember.CollectionType.Series,
                        CollectionPosition = seq.Number
                    };
                    epubFile.AddNewCollection(collectionMember);
                    foreach (var subseq in seq.SubSections.Where(subseq => !string.IsNullOrEmpty(subseq.Name)))
                    {
                        collectionMember = new SeriesCollectionMember
                        {
                            CollectionName = subseq.Name,
                            Type = SeriesCollectionMember.CollectionType.Set,
                            CollectionPosition = subseq.Number
                        };
                        epubFile.AddNewCollection(collectionMember);
                    }
                }
            }
            foreach (var seq in fb2File.PublishInfo.Sequences)
            {
                if (!string.IsNullOrEmpty(seq.Name))
                {
                    var collectionMember = new SeriesCollectionMember
                    {
                        CollectionName = seq.Name,
                        Type = SeriesCollectionMember.CollectionType.Series,
                        CollectionPosition = seq.Number
                    };
                    epubFile.AddNewCollection(collectionMember);
                    foreach (var subseq in seq.SubSections.Where(subseq => !string.IsNullOrEmpty(subseq.Name)))
                    {
                        collectionMember = new SeriesCollectionMember
                        {
                            CollectionName = subseq.Name,
                            Type = SeriesCollectionMember.CollectionType.Set,
                            CollectionPosition = subseq.Number
                        };
                        epubFile.AddNewCollection(collectionMember);
                    }
                }
            }
        }

    }
}
