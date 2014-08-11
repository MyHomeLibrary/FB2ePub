using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.Events;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The img element is used to define an image.
    /// </summary>
    public class Image : BaseInlineItem
    {
        public const string ElementName = "img";


        private readonly AltAttribute _altAttribute = new AltAttribute();
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();
        private readonly SourceAttribute _srcAttribute = new SourceAttribute();
        private readonly ISMapAttribute _ismapAttribute = new ISMapAttribute();
        private readonly UseMapAttribute _useMapAttribute = new UseMapAttribute();
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();
        private readonly CrossOriginAttribute _crossOriginAttribute = new CrossOriginAttribute();


        #region public_attributes


        /// <summary>
        /// Alternate text for the image. This attribute is required. 
        /// The value should be left blank for decorative images.
        /// </summary>
        public AltAttribute Alt
        {
            get
            {
                return _altAttribute;
            }
        }

        /// <summary>
        /// Image height.
        /// </summary>
        public HeightAttribute Height
        {
            get { return _heightAttribute; }
        }

        /// <summary>
        /// Image width.
        /// </summary>
        public  WidthAttribute Width
        {
            get { return _widthAttribute; }
        }

        /// <summary>
        /// This required attribute specifies the location of the image source.
        /// </summary>
        public  SourceAttribute Source
        {
            get { return _srcAttribute; }
        }

        /// <summary>
        /// If present, this attribute specifies that a server-side image map should be used. 
        /// Possible value is "ismap".
        /// </summary>
        public ISMapAttribute ISMap
        {
            get { return _ismapAttribute; }
        }


        /// <summary>
        /// This attribute associates the image to a client-side image map defined by a map element. 
        /// The value of this attribute must match the id attribute of the map element.
        /// </summary>
        public UseMapAttribute UseMap
        {
            get { return _useMapAttribute; }
        }

        /// <summary>
        /// Allow images from third-party sites that allow cross-origin access to be used with canvas
        /// </summary>
        public CrossOriginAttribute Crossorigin { get { return _crossOriginAttribute; }}

        #endregion




        #region Overrides of BaseInlineItem

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
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }

            ReadAttributes(xElement);
        }

        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>
        /// generated XNode
        /// </returns>
        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);
            return xElement;
        }

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return (_srcAttribute.HasValue());
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

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _altAttribute.AddAttribute(xElement);
            _heightAttribute.AddAttribute(xElement);
            _widthAttribute.AddAttribute(xElement);
            _srcAttribute.AddAttribute(xElement);
            _crossOriginAttribute.AddAttribute(xElement);
            _ismapAttribute.AddAttribute(xElement);
            _useMapAttribute.AddAttribute(xElement);
       }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _altAttribute.ReadAttribute(xElement);
            _heightAttribute.ReadAttribute(xElement);
            _widthAttribute.ReadAttribute(xElement);
            _srcAttribute.ReadAttribute(xElement);
            _crossOriginAttribute.ReadAttribute(xElement);
            _ismapAttribute.ReadAttribute(xElement);
            _useMapAttribute.ReadAttribute(xElement);
        }

        #endregion
    }
}
