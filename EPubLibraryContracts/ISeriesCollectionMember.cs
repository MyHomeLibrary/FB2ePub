using System.Xml.Linq;

namespace EPubLibraryContracts
{
    /// <summary>
    /// Types of the collections
    /// </summary>
    public enum CollectionType
    {
        Series, // top level series ( A sequence of related works that are formally identified as a group; typically open-ended with works issued individually over time.)
        Set,    // sub- "series" (A finite collection of works that together constitute a single intellectual unit; typically issued together and able to be sold as a unit)
    }

    public interface ISeriesCollectionMember
    {
        string CollectionName { get; set; }
        CollectionType Type { get; set; }
        int? CollectionPosition { get; set; }
        string CollectionUID { get; set; }

        void AddCollectionToElement(XElement metadata, int collectionCounter);
    }

    public interface ISeriesCollection
    {
        void AddCollectionMember(ISeriesCollectionMember newMemeber);
    }

}
