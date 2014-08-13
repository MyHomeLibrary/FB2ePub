using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace HTML5ClassLibrary.BaseElements
{
    /// <summary>
    /// Interface defining behavior of basic HTML 5 item
    /// </summary>
    public interface IHTML5Item
    {
        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
        void Load(XNode xNode);

        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>generated XNode</returns>
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
        void Add(IHTML5Item item);

        /// <summary>
        /// Removes sub item 
        /// </summary>
        /// <param name="item">sub item to remove</param>
        void Remove(IHTML5Item item);

        /// <summary>
        /// Get list of all sub elements
        /// </summary>
        /// <returns></returns>
        List<IHTML5Item> SubElements();

        /// <summary>
        /// Get/Set item parent in the HTML5 "tree"
        /// </summary>
        IHTML5Item Parent { get; set; }


    }
}
