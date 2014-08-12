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
    public class Title : HTML5Item
    {
        public const string ElementName = "title";

        private readonly SimpleHTML5Text _content = new SimpleHTML5Text();


        /// <summary>
        /// Returns title text
        /// </summary>
        public SimpleHTML5Text Content { get { return _content; } }



        /// <summary>
        /// Specifies an absolute URL that acts as the base URL for resolving relative URLs. 
        /// This attribute is required.
        /// </summary>
        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";


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

            xElement.Add(_content.Generate());

            AddAttributes(xElement);

            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }

    }
}
