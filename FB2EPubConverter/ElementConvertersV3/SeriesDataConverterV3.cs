using System.Linq;
using EPubLibrary.Content.Collections;
using EPubLibraryContracts;
using FB2Library;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class SeriesDataConverterV3
    {
        public void Convert(FB2File fb2File, IBookInformationData titleInformation)
        {
            foreach (var seq in fb2File.TitleInfo.Sequences)
            {
                if (!string.IsNullOrEmpty(seq.Name))
                {
                    var collectionMember = new SeriesCollectionMember
                    {
                        CollectionName = seq.Name,
                        Type = CollectionType.Series,
                        CollectionPosition = seq.Number
                    };
                    titleInformation.SeriesCollection.AddCollectionMember(collectionMember);
                    foreach (var subseq in seq.SubSections.Where(subseq => !string.IsNullOrEmpty(subseq.Name)))
                    {
                        collectionMember = new SeriesCollectionMember
                        {
                            CollectionName = subseq.Name,
                            Type = CollectionType.Set,
                            CollectionPosition = subseq.Number
                        };
                        titleInformation.SeriesCollection.AddCollectionMember(collectionMember);
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
                        Type = CollectionType.Series,
                        CollectionPosition = seq.Number
                    };
                    titleInformation.SeriesCollection.AddCollectionMember(collectionMember);
                    foreach (var subseq in seq.SubSections.Where(subseq => !string.IsNullOrEmpty(subseq.Name)))
                    {
                        collectionMember = new SeriesCollectionMember
                        {
                            CollectionName = subseq.Name,
                            Type = CollectionType.Set,
                            CollectionPosition = subseq.Number
                        };
                        titleInformation.SeriesCollection.AddCollectionMember(collectionMember);
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
                        Type = CollectionType.Series,
                        CollectionPosition = seq.Number
                    };
                    titleInformation.SeriesCollection.AddCollectionMember(collectionMember);
                    foreach (var subseq in seq.SubSections.Where(subseq => !string.IsNullOrEmpty(subseq.Name)))
                    {
                        collectionMember = new SeriesCollectionMember
                        {
                            CollectionName = subseq.Name,
                            Type = CollectionType.Set,
                            CollectionPosition = subseq.Number
                        };
                        titleInformation.SeriesCollection.AddCollectionMember(collectionMember);
                    }
                }
            }
        }

    }
}
