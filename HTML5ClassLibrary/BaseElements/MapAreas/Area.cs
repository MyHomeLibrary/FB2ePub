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

namespace HTML5ClassLibrary.BaseElements.MapAreas
{
    /// <summary>
    /// The area element identifies geometric regions of a client-side image map, and provides a hyperlink for each region.
    /// </summary>
    public class Area : HTML5Item
    {
        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";

        private readonly AltAttribute _altAttribute = new AltAttribute();
        private readonly CoordinatesAttribute _coordAttribute = new CoordinatesAttribute();
        private readonly DownloadAttribute  _downloadAttribute = new DownloadAttribute();
        private readonly HrefAttribute _hrefAttribute = new HrefAttribute();
        private readonly HRefLanguageAttribute _hrefLangAttribute = new HRefLanguageAttribute();
        private readonly MediaAttribute _mediaAttribute = new MediaAttribute();
        private readonly AreaRelationAttribute _relationAttribute = new AreaRelationAttribute();
        private readonly ShapeAttribute _shapeAttribute = new ShapeAttribute();
        private readonly FormTargetAttribute _targetAttribute = new FormTargetAttribute();
        private readonly MIMETypeAttribute  _typeAttribute = new MIMETypeAttribute();


        public const string ElementName = "area";



        /// <summary>
        /// Alternate text. This attribute is required.
        /// </summary>
        public AltAttribute Alt { get { return _altAttribute; } }


        /// <summary>
        /// Specifies the position and shape on the screen. 
        /// The number and order of values depends on the shape being defined. 
        /// Possible combinations:
        /// * rect: left-x, top-y, right-x, bottom-y.
        /// * circle: center-x, center-y, radius.
        /// * poly: x1, y1, x2, y2, ..., xN, yN. 
        /// The first and the last x and y coordinate pair should be the same, in order to close the polygon.
        /// Coordinates are relative to the top-left corner of the object. All values are separated by commas.
        /// </summary>
        public CoordinatesAttribute Coords { get { return _coordAttribute; } }

        /// <summary>
        /// Specifies that the target will be downloaded when a user clicks on the hyperlink
        /// </summary>
        public DownloadAttribute Download { get { return _downloadAttribute; }}

        /// <summary>
        /// Specifies the location of a Web resource.
        /// </summary>
        public HrefAttribute HRef { get { return _hrefAttribute; } }

        /// <summary>
        /// Specifies the language of the target URL
        /// </summary>
        public HRefLanguageAttribute HRefLang { get { return _hrefLangAttribute; }}

        /// <summary>
        /// Specifies what media/device the target URL is optimized for
        /// </summary>
        public MediaAttribute Media { get { return _mediaAttribute; }}

        /// <summary>
        /// The rel attribute specifies the relationship between the current document and the linked document.
        /// Only used if the href attribute is present.
        /// </summary>
        public AreaRelationAttribute Rel { get { return _relationAttribute; } }

        /// <summary>
        /// Specifies the shape of a region. 
        /// Possible values are:
        ///  * default: Specifies the entire region
        /// * rect: Defines a rectangular region.
        /// * circle: Defines a circular region.
        /// * poly: Defines a polygonal region.
        /// </summary>
        public ShapeAttribute Shape { get { return _shapeAttribute; } }

        /// <summary>
        /// Specifies where to open the target URL
        /// </summary>
        public FormTargetAttribute Target { get { return _targetAttribute; } }

        /// <summary>
        /// Specifies the MIME type of the target URL
        /// </summary>
        public MIMETypeAttribute Type { get { return _typeAttribute; }}

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
        /// <returns>generated XNode</returns>
        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            return xElement;
        }

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _altAttribute.AddAttribute(xElement);
            _coordAttribute.AddAttribute(xElement);
            _downloadAttribute.AddAttribute(xElement);
            _hrefAttribute.AddAttribute(xElement);
            _hrefLangAttribute.AddAttribute(xElement);
            _mediaAttribute.AddAttribute(xElement);
            _relationAttribute.AddAttribute(xElement);
            _shapeAttribute.AddAttribute(xElement);
            _targetAttribute.AddAttribute(xElement);
            _typeAttribute.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _altAttribute.ReadAttribute(xElement);
            _coordAttribute.ReadAttribute(xElement);
            _downloadAttribute.ReadAttribute(xElement);
            _hrefAttribute.ReadAttribute(xElement);
            _hrefLangAttribute.ReadAttribute(xElement);
            _mediaAttribute.ReadAttribute(xElement);
            _relationAttribute.ReadAttribute(xElement);
            _shapeAttribute.ReadAttribute(xElement);
            _targetAttribute.ReadAttribute(xElement);
            _typeAttribute.ReadAttribute(xElement);
        }

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public  override bool IsValid()
        {
            return (_altAttribute.HasValue());
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


        #endregion
    }
}