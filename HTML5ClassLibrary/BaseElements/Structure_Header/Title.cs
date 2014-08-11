using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;

namespace HTML5ClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The "title" element is used to identify the document.
    /// </summary>
    public class Title : IHTML5Item
    {
        public const string ElementName = "title";

        private readonly SimpleHTML5Text _content = new SimpleHTML5Text();

        private readonly HTMLGlobalAttributes _globalAttributes = new HTMLGlobalAttributes();
        private readonly FormEvents _formEvents = new FormEvents();
        private readonly KeyboardEvents _keyboardEvents = new KeyboardEvents();
        private readonly MediaEvents _mediaEvents = new MediaEvents();
        private readonly MouseEvents _mouseEvents = new MouseEvents();
        private readonly WindowEventAttributes _windowEventAttributes = new WindowEventAttributes();


        /// <summary>
        /// Returns title text
        /// </summary>
        public SimpleHTML5Text Content { get { return _content; } }


        public HTMLGlobalAttributes GlobalAttributes { get { return _globalAttributes; }}

        public FormEvents FormEvents { get { return _formEvents; } }

        public KeyboardEvents KeyboardEvents { get { return _keyboardEvents; } }

        public MediaEvents MediaEvents { get { return _mediaEvents; } }

        public MouseEvents MouseEvents { get { return _mouseEvents; } }

        public WindowEventAttributes WindowEvents { get { return _windowEventAttributes; } }


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
            _globalAttributes.ReadAttributes(xElement);
            _formEvents.ReadAttributes(xElement);
            _keyboardEvents.ReadAttributes(xElement);
            _mediaEvents.ReadAttributes(xElement);
            _mouseEvents.ReadAttributes(xElement);
            _windowEventAttributes.ReadAttributes(xElement);
            _content.Load(xNode);
        }

        public XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            xElement.Add(_content.Generate());

            _globalAttributes.AddAttributes(xElement);
            _formEvents.AddAttributes(xElement);
            _keyboardEvents.AddAttributes(xElement);
            _mediaEvents.AddAttributes(xElement);
            _mouseEvents.AddAttributes(xElement);
            _windowEventAttributes.AddAttributes(xElement);
            return xElement;
        }

        public bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
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
