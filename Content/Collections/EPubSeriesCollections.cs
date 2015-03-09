using System.Collections.Generic;
using System.Xml.Linq;
using EPubLibraryContracts;

namespace EPubLibrary.Content.Collections
{


    /// <summary>
    /// Contains "collections" - series etc
    /// </summary>
    public class EPubSeriesCollections : ISeriesCollection
    {
        private readonly List<ISeriesCollectionMember>   _collectionMembers = new List<ISeriesCollectionMember>();

       
        /// <summary>
        /// Writes the collections information to metadata element
        /// </summary>
        /// <param name="metadata"></param>
        public void AddCollectionsToElement(XElement metadata)
        {
            int collectionCounter = 0;
            foreach (var collection in _collectionMembers)
            {
                collection.AddCollectionToElement(metadata, ++collectionCounter);
            }

        }

        public void AddCollectionMember(ISeriesCollectionMember newMemeber)
        {
            _collectionMembers.Add(newMemeber);
        }
    }
}
