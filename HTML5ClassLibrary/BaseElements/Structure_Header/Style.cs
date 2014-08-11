﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;

namespace HTML5ClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The style element can contain CSS rules (called embedded CSS) or 
    /// a URL that leads to a file containing CSS rules (called external CSS).
    /// </summary>
    public class Style : IHTML5Item
    {
        internal const string ElementName = "style";

        private readonly SimpleHTML5Text _content = new SimpleHTML5Text();

        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

        private readonly MediaAttribute _mediaAttribute = new MediaAttribute();
        private readonly ContentTypeAttribute _typeAttribute = new ContentTypeAttribute();
        private readonly ScopedAttribute _scopedAttribute = new ScopedAttribute();
        private readonly HTMLGlobalAttributes _globalAttributes = new HTMLGlobalAttributes();
        private readonly FormEvents _formEvents = new FormEvents();
        private readonly KeyboardEvents _keyboardEvents = new KeyboardEvents();
        private readonly MediaEvents _mediaEvents = new MediaEvents();
        private readonly MouseEvents _mouseEvents = new MouseEvents();
        private readonly WindowEventAttributes _windowEventAttributes = new WindowEventAttributes();

        
        public SimpleHTML5Text Content
        {
            get { return _content; }
        }


        public HTMLGlobalAttributes GlobalAttributes { get { return _globalAttributes; }}

        public FormEvents FormEvents { get { return _formEvents; } }

        public KeyboardEvents KeyboardEvents { get { return _keyboardEvents; } }

        public MediaEvents MediaEvents { get { return _mediaEvents; } }

        public MouseEvents MouseEvents { get { return _mouseEvents; } }

        public WindowEventAttributes WindowEvents { get { return _windowEventAttributes; } }


        /// <summary>
        /// This attribute specifies the intended destination medium for style information. 
        /// It may be a single media descriptor or a comma-separated list. 
        /// The default value for this attribute is screen.
        /// </summary>
        public MediaAttribute Media { get { return _mediaAttribute; } }

        /// <summary>
        /// This attribute specifies the style sheet language of the element's contents. 
        /// The style sheet language is specified as a content type. 
        /// For example: text/css. 
        /// This attribute is required.
        /// </summary>
        public ContentTypeAttribute Type { get { return _typeAttribute; } }

        /// <summary>
        /// Specifies that the styles only apply to this element's parent element and that element's child elements
        /// </summary>
        public ScopedAttribute Scoped { get { return _scopedAttribute; }}


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
            _mediaAttribute.ReadAttribute(xElement);
            _scopedAttribute.ReadAttribute(xElement);
            _typeAttribute.ReadAttribute(xElement);

            _content.Load(xNode);
        }

        public XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            _globalAttributes.AddAttributes(xElement);
            _formEvents.AddAttributes(xElement);
            _keyboardEvents.AddAttributes(xElement);
            _mediaEvents.AddAttributes(xElement);
            _mouseEvents.AddAttributes(xElement);
            _windowEventAttributes.AddAttributes(xElement);
            _mediaAttribute.AddAttribute(xElement);
            _scopedAttribute.AddAttribute(xElement);
            _typeAttribute.AddAttribute(xElement);

            xElement.Add(_content.Generate());
            return xElement;

        }


        public bool IsValid()
        {
            return _typeAttribute.HasValue();
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
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