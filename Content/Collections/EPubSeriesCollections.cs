using System.Collections.Generic;
using System.Xml.Linq;

namespace EPubLibrary.Content.Collections
{


    /// <summary>
    /// Contains "collections" - series etc
    /// </summary>
    public class EPubSeriesCollections
    {
        private readonly List<SeriesCollectionMember>   _collectionMembers = new List<SeriesCollectionMember>();

        /// <summary>
        /// List of collection members
        /// </summary>
        public List<SeriesCollectionMember> CollectionMembers
        {
            get{return _collectionMembers;}
        }

        /// <summary>
        /// Writes the collections information to metadata element
        /// </summary>
        /// <param name="metadata"></param>
        public void AddCollectionsToElement(XElement metadata)
        {
            int collectionCounter = 0;
            foreach (var collection in CollectionMembers)
            {
                collection.AddCollectionToElement(metadata, ++collectionCounter);
            }

        }
    }
}
