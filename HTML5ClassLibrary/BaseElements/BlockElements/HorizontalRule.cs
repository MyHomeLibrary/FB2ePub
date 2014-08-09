using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The hr element is used to separate sections of content. 
    /// Though the name of the hr element is "horizontal rule", most visual Web browsers render hr as a horizontal line.
    /// </summary>
    public class HorizontalRule : BaseBlockElement 
    {
        internal const string ElementName = "hr";

        #region Overrides of BaseBlockElement

        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            XElement xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }

            ReadAttributes(xElement);

        }


        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>generated XNode</returns>
        public override XNode Generate()
        {
            XElement xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            return xElement;
        }

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public new void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public new void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public new List<IHTML5Item> SubElements()
        {
            return null;
        }

        /// <summary>
        /// Check if element can be sub-element of this element (according to XHTML rules)
        /// </summary>
        /// <param name="item">element to check</param>
        /// <returns>true if it can be sub-element, false otherwise</returns>
        protected override bool IsValidSubType(IHTML5Item item)
        {
            return false;
        }
        #endregion

    }
}
