using System;
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
    public class Style : HTML5Item
    {
        internal const string ElementName = "style";

        private readonly SimpleHTML5Text _content = new SimpleHTML5Text();

        public Style()
        {
            Attributes.Add(_mediaAttribute);
            Attributes.Add(_typeAttribute);
            Attributes.Add(_scopedAttribute);
        }

        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

        private readonly MediaAttribute _mediaAttribute = new MediaAttribute();
        private readonly ContentTypeAttribute _typeAttribute = new ContentTypeAttribute();
        private readonly ScopedAttribute _scopedAttribute = new ScopedAttribute();

        
        public SimpleHTML5Text Content
        {
            get { return _content; }
        }


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


        public override void Load(XNode xNode)
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

            ReadAttributes(xElement);

            _content.Load(xNode);
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            xElement.Add(_content.Generate());
            return xElement;
        }


        public override bool IsValid()
        {
            return _typeAttribute.HasValue();
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain subitems");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain subitems");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }

    }
}
