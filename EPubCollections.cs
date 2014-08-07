using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPubLibrary
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
    /// Coolection member
    /// </summary>
    public class CollectionMember
    {
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
        public  string CollectionUID { get; set; }

        public static string ToStringType(CollectionType collectionType)
        {
            if (collectionType == CollectionType.Series)
            {
                return "series";
            }
            return "set";
        }
    }


    /// <summary>
    /// Contains "collections" - series etc
    /// </summary>
    public class EPubCollections
    {
        protected readonly List<CollectionMember>   _CollectionMembers = new List<CollectionMember>();

        /// <summary>
        /// List of collection members
        /// </summary>
        public List<CollectionMember> CollectionMembers
        {
            get{return _CollectionMembers;}
        }
    }
}
