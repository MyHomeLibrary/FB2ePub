using System.Collections.Generic;
using System.Xml.Linq;


namespace HTMLClassLibrary.BaseElements
{
    public interface ISimpleText : IHTMLItem
    {
        string Text { get; set; }
    }


    /// <summary>
    /// Interface defining behavior of basic HTML 5 item
    /// </summary>
    public interface IHTMLItem
    {
        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
        void Load(XNode xNode);

        /// <summary>
        /// Create XElement element and serializes data to it
        /// </summary>
        /// <returns>generated XElement</returns>
        XNode Generate();

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        bool IsValid();

        /// <summary>
        /// Adds sub item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub item to add</param>
        void Add(IHTMLItem item);

        /// <summary>
        /// Removes sub item 
        /// </summary>
        /// <param name="item">sub item to remove</param>
        void Remove(IHTMLItem item);

        /// <summary>
        /// Get list of all sub elements
        /// </summary>
        /// <returns></returns>
        List<IHTMLItem> SubElements();

        /// <summary>
        /// Get/Set item parent in the HTML5 "tree"
        /// </summary>
        IHTMLItem Parent { get; set; }

        /// <summary>
        /// Internal text item if available
        /// </summary>
        ISimpleText InternalTextItem { get; }


    }
}
