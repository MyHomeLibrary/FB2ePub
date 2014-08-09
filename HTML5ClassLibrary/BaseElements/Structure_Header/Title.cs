using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;

namespace HTML5ClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The "title" element is used to identify the document.
    /// </summary>
    public class Title : IHTML5Item
    {
        private const string ElementName = "title";

        private readonly LanguageAttr _language = new LanguageAttr();
        private readonly DirectionAttr _direction = new DirectionAttr();

        private readonly SimpleHTML5Text _content = new SimpleHTML5Text();


        /// <summary>
        /// Returns title text
        /// </summary>
        public SimpleHTML5Text Content { get { return _content; } }

        /// <summary>
        /// This attribute specifies the base language of an element's attribute values and text content.
        /// </summary>
        public LanguageAttr Language
        {
            get { return _language; }
        }

        /// <summary>
        /// This attribute specifies the base direction of text. 
        /// Possible values:
        /// ltr: Left-to-right 
        /// rtl: Right-to-left
        /// </summary>
        public DirectionAttr Direction
        {
            get { return _direction; }
        }

        /// <summary>
        /// Specifies an absolute URL that acts as the base URL for resolving relative URLs. 
        /// This attribute is required.
        /// </summary>
        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";


        public void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }

            _language.ReadAttribute(xElement);
            _direction.ReadAttribute(xElement);

            _content.Load(xNode);
        }

        public XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            _language.AddAttribute(xElement);
            _direction.AddAttribute(xElement);

            xElement.Add(_content.Generate());

            return xElement;
        }

        public bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds subitem to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">subitem to add</param>
        public void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain subitems");
        }

        public void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain subitems");
        }

        public List<IHTML5Item> SubElements()
        {
            return null;
        }

        /// <summary>
        /// Get/Set item parent in the XHTML "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }
    }
}
