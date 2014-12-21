using System.Xml.Linq;

namespace EPubLibrary.Content.Collections
{

    /// <summary>
    /// Coolection member
    /// </summary>
    public class SeriesCollectionMember
    {
        /// <summary>
        /// Types of the collections
        /// </summary>
        public enum CollectionType
        {
            Series, // top level series ( A sequence of related works that are formally identified as a group; typically open-ended with works issued individually over time.)
            Set,    // sub- "series" (A finite collection of works that together constitute a single intellectual unit; typically issued together and able to be sold as a unit)
        }

        /// <summary>
        /// Name of the collection publication belongs to
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// Type of the series (author/publisher)
        /// </summary>
        public CollectionType Type { get; set; }

        /// <summary>
        /// Number in collection if present
        /// </summary>
        public int? CollectionPosition { get; set; }

        /// <summary>
        /// Collection UID, not present in FB2
        /// </summary>
        public string CollectionUID { get; set; }

        public static string ToStringType(CollectionType collectionType)
        {
            if (collectionType == CollectionType.Series)
            {
                return "series";
            }
            return "set";
        }

        public void AddCollectionToElement(XElement metadata,int collectionCounter)
        {
            string collectionID = string.Format("collect_{0}", ++collectionCounter);
            var metaBelongsTo = new XElement(EPubNamespaces.FakeOpf + "meta", CollectionName);
            metaBelongsTo.Add(new XAttribute("property", "belongs-to-collection"));
            metaBelongsTo.Add(new XAttribute("id", collectionID));
            metadata.Add(metaBelongsTo);

            var metaCollectionType = new XElement(EPubNamespaces.FakeOpf + "meta", ToStringType(Type));
            metaCollectionType.Add(new XAttribute("property", "collection-type"));
            metaCollectionType.Add(new XAttribute("refines", "#" + collectionID));
            metadata.Add(metaCollectionType);

            if (CollectionPosition.HasValue)
            {
                var metaPosition = new XElement(EPubNamespaces.FakeOpf + "meta", CollectionPosition.Value);
                metaPosition.Add(new XAttribute("property", "group-position"));
                metaPosition.Add(new XAttribute("refines", "#" + collectionID));
                metadata.Add(metaPosition);
            }

            if (!string.IsNullOrEmpty(CollectionUID))
            {
                var metaUID = new XElement(EPubNamespaces.FakeOpf + "meta", CollectionUID);
                metaUID.Add(new XAttribute("property", "dcterms:identifier"));
                metaUID.Add(new XAttribute("refines", "#" + collectionID));
                metadata.Add(metaUID);

            }

        }
    }
}
