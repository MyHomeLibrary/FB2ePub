using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace HTML5ClassLibrary.BaseElements
{
    public enum TextStyles
    {
        Normal = 0,
        Strong, // <strong> , deprecated <b>
        Emphasis, // <em>
        Sub, // <sub>
        Sup, // <sup>
        Big, // <big>
        Small, // <small>
        StrikeOut, // <strike>
        Code, // <code>
    }


    /// <summary>
    /// Represent a simple HTML 5 text element (XText)
    /// </summary>
    public class SimpleHTML5Text : IHTML5Item
    {
        /// <summary>
        /// Textual data contained in element
        /// </summary>
        private string _text;

        /// <summary>
        /// Get/Set actual text contained in element
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value ?? string.Empty; }
        }

        /// <summary>
        /// Loads a text element from the HTML node
        /// </summary>
        /// <param name="xNode"></param>
        public void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Text)
            {
                throw new ArgumentException("xNode is not of text type", "xNode");
            }
            Text = ((XText)xNode).Value;
        }

        /// <summary>
        /// Creates a XText node from the contained text
        /// </summary>
        /// <returns></returns>
        public XNode Generate()
        {
            return new XText(_text);
        }

        /// <summary>
        /// Check if element is valid
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return (_text != null);
        }

        /// <summary>
        /// Adds sub item to the item , only if 
        /// allowed by the rules and element can accept content
        /// simple text can't have sub-items so it will always return false
        /// </summary>
        /// <param name="item">sub item to add</param>
        public void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain or obtain sub items");
        }

        /// <summary>
        /// Remove "unlink" sub-item 
        /// useless here as can't have sub-items at all
        /// </summary>
        /// <param name="item"></param>
        public void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain or obtain sub items");
        }

        /// <summary>
        /// Return list of sub-items,
        /// useless here as can not contain sub-items
        /// </summary>
        /// <returns></returns>
        public List<IHTML5Item> SubElements()
        {
            return null;
        }

        /// <summary>
        /// Get/Set item parent "node" (IHTML5Item) in the HTML 5 "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }
    }
}
