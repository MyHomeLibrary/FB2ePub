using System.Collections.Generic;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;

namespace HTML5ClassLibrary.BaseElements.TableElements
{
    public interface ITableElement : IHTML5Item
    {

    }

    /// <summary>
    /// Base class for all table elements
    /// </summary>
    public abstract class BaseTableElement : ITableElement
    {

        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

        private readonly HTMLGlobalAttributes _globalAttributes = new HTMLGlobalAttributes();


        public HTMLGlobalAttributes GlobalAttributes { get { return _globalAttributes; }}

        #region Implementation of IHTML5Item

        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
        public abstract void Load(XNode xNode);

        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>generated XNode</returns>
        public abstract XNode Generate();

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public abstract bool IsValid();

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public abstract void Add(IHTML5Item item);

        public abstract void Remove(IHTML5Item item);

        public abstract List<IHTML5Item> SubElements();


        /// <summary>
        /// Get/Set item parent in the XHTML "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }

        #endregion

        protected virtual void AddAttributes(XElement xElement)
        {
             _globalAttributes.AddAttributes(xElement);
        }

        protected virtual void ReadAttributes(XElement xElement)
        {
            _globalAttributes.ReadAttributes(xElement);
        }
    }
}
