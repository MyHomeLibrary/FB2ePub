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

namespace HTML5ClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The link element conveys relationship information that can be used by Web browsers and search engines. 
    /// You can have multiple link elements that link to different resources or describe different relationships. 
    /// The link elements can be contained in the head element.
    /// </summary>
    public class Link : HTML5Item
    {
        internal const string ElementName = "link";

        private readonly HrefAttribute _hrefAttribute = new HrefAttribute();
        private readonly HRefLanguageAttribute _hrefLangAttribute = new HRefLanguageAttribute();
        private readonly MediaAttribute _mediaAttribute = new MediaAttribute();
        private readonly LinkRelationAttribute _relAttribute = new LinkRelationAttribute();
        private readonly MIMETypeAttribute _typeAttribute = new MIMETypeAttribute();
        private readonly SizesAttribute _sizesAttribute = new SizesAttribute();


        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

        #region public_properties


        /// <summary>
        /// Specifies the primary language of the resource designated by href and may only be used when href is specified.
        /// </summary>
        public HRefLanguageAttribute RefLanguage { get { return _hrefLangAttribute;}}

        /// <summary>
        /// Describes the forward relationship from the current document to the resource specified by the href attribute. 
        /// The value of this attribute is a space-separated list of link types.
        /// </summary>
        public LinkRelationAttribute Relation { get { return _relAttribute; } }

        /// <summary>
        /// This attribute specifies the location of a Web resource.
        /// </summary>
        public HrefAttribute HRef { get { return _hrefAttribute; } }

        /// <summary>
        /// his attribute specifies the intended destination medium for style information. 
        /// It may be a single media descriptor or a comma-separated list. 
        /// The default value for this attribute is "screen".
        /// </summary>
        public MediaAttribute Media { get { return _mediaAttribute; } }

        /// <summary>
        /// Style sheet language. 
        /// For example: text/css.
        /// </summary>
        public MIMETypeAttribute Type { get { return _typeAttribute; } }


        /// <summary>
        /// Specifies the size of the linked resource. Only for rel="icon"
        /// </summary>
        public SizesAttribute Sizes { get { return _sizesAttribute; }}


        #endregion

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
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _hrefAttribute.ReadAttribute(xElement);
            _mediaAttribute.ReadAttribute(xElement);
            _typeAttribute.ReadAttribute(xElement);
            _sizesAttribute.ReadAttribute(xElement);
            _hrefLangAttribute.ReadAttribute(xElement);
            _relAttribute.ReadAttribute(xElement);
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);
            return xElement;

        }

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _hrefAttribute.AddAttribute(xElement);
            _mediaAttribute.AddAttribute(xElement);
            _typeAttribute.AddAttribute(xElement);
            _sizesAttribute.AddAttribute(xElement);
            _hrefLangAttribute.AddAttribute(xElement);
            _relAttribute.AddAttribute(xElement);
        }

        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds subitem to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">subitem to add</param>
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
