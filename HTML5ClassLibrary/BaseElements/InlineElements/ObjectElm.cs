using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.Events;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;
using HTML5ClassLibrary.BaseElements.BlockElements;
using HTML5ClassLibrary.BaseElements.ObjectParameters;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The object element provides a generic way of embedding objects such as images, 
    /// movies and applications (Java applets, browser plug-ins, etc.) into Web pages. 
    /// param elements contained inside the object element are used to configure the embedded object. 
    /// Besides param elements, the object element can contain alternate content which can be text or another object element. 
    /// Alternate content serves as a fall-back mechanism for browsers that are unable to process the embedded object.
    /// </summary>
    public class ObjectElm : BaseInlineItem
    {
        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTML5Item> _content = new List<IHTML5Item>();


        // Base attributes
        private readonly ClassIDAttribute _classIdAttribute = new ClassIDAttribute();
        private readonly CodeBaseAttribute _codeBaseAttribute = new CodeBaseAttribute();
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly ContentTypeAttribute _contentTypeAttribute = new ContentTypeAttribute();
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();

        // Advanced attributes
        private readonly ArchiveAttribute _archiveAttribute = new ArchiveAttribute();
        private readonly CodeTypeAttribute _codeTypeAttribute = new CodeTypeAttribute();
        private readonly DataAttribute _dataAttribute = new DataAttribute();
        private readonly DeclareAttribute _declareAttribute = new DeclareAttribute();
        private readonly StandByAttribute _standbyAttribute = new StandByAttribute();
        private readonly TabIndexAttribute _tabIndexAttribute = new TabIndexAttribute();
        private readonly UseMapAttribute _useMapAttribute = new UseMapAttribute();


        private readonly LanguageAttr _language = new LanguageAttr();
        private readonly DirectionAttr _direction = new DirectionAttr();


        // Common event attributes
        private readonly OnClickEventAttribute _onClick = new OnClickEventAttribute();
        private readonly OnDblClickEventAttribute _onDblClick = new OnDblClickEventAttribute();
        private readonly OnMouseDownEventAttribute _onMouseDown = new OnMouseDownEventAttribute();
        private readonly OnMouseUpEventAttribute _onMouseUp = new OnMouseUpEventAttribute();
        private readonly OnMouseOverEventAttribute _onMouseOver = new OnMouseOverEventAttribute();
        private readonly OnMouseMoveEventAttribute _onMouseMove = new OnMouseMoveEventAttribute();
        private readonly OnMouseOutEventAttribute _onMouseOut = new OnMouseOutEventAttribute();
        private readonly OnKeyPressEventAttribute _onKeyPress = new OnKeyPressEventAttribute();
        private readonly OnKeyDownEventAttribute _onKeyDown = new OnKeyDownEventAttribute();
        private readonly OnKeyUpEventAttribute _onKeyUp = new OnKeyUpEventAttribute();


        public const string ElementName = "object";


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
        /// A client-side script event that occurs when a pointing device button is clicked over an element.
        /// </summary>
        public OnClickEventAttribute OnClick
        {
            get { return _onClick; }
        }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is double-clicked over an element.
        /// </summary>
        public OnDblClickEventAttribute OnDblClick { get { return _onDblClick; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is pressed down over an element.
        /// </summary>
        public OnMouseDownEventAttribute OnMouseDown { get { return _onMouseDown; } }

        /// <summary>
        /// A client-side script event that occurs when a pointing device button is released over an element.
        /// </summary>
        public OnMouseUpEventAttribute OnMouseUp { get { return _onMouseUp; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved onto an element.
        /// </summary>
        public OnMouseOverEventAttribute OnMouseOver { get { return _onMouseOver; } }

        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved within an element.
        /// </summary>
        public OnMouseMoveEventAttribute OnMouseMove { get { return _onMouseMove; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved away from an element.
        /// </summary>
        public OnMouseOutEventAttribute OnMouseOut { get { return _onMouseOut; } }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element then released.
        /// </summary>
        public OnKeyPressEventAttribute OnKeyPress { get { return _onKeyPress; } }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element.
        /// </summary>
        public OnKeyDownEventAttribute OnKeyDown { get { return _onKeyDown; } }

        /// <summary>
        /// A client-side script event that occurs when a key is released over an element.
        /// </summary>
        public OnKeyUpEventAttribute OnKeyUp { get { return _onKeyUp; } }

        /// <summary>
        /// This attribute specifies the base language of an element's attribute values and text content.
        /// </summary>
        public LanguageAttr Language
        {
            get { return _language; }
        }

        /// <summary>
        /// This attribute may be used to specify the location of an object's implementation via a URI. 
        /// It may be used together with, or as an alternative to, the data attribute, depending on the type of object involved. 
        /// For example: clsid:D27CDB6E-AE6D-11cf-96B8-444553540000.
        /// </summary>
        public ClassIDAttribute ClassId { get { return _classIdAttribute; } }

        /// <summary>
        /// This attribute specifies the base path used to resolve relative URIs specified by the classid, data, and archive attributes. 
        /// When absent, its default value is the base URI of the current document. 
        /// Some Web browser like Internet Explorer use this attribute to point to CAB or INF files in order to download the object.
        /// </summary>
        public CodeBaseAttribute CodeBase { get { return _codeBaseAttribute; } }

        /// <summary>
        /// Object height.
        /// </summary>
        public HeightAttribute Height { get { return _heightAttribute; } }

        /// <summary>
        /// When the object is used as a form control, this attribute is the name of the form control.
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; } }

        /// <summary>
        /// This attribute specifies the content type for the object. 
        /// This attribute may be used with or without the data attribute. 
        /// Most Web browsers use this attribute (instead of classid) to determine how to process the object. 
        /// For example: application/x-shockwave-flash.
        /// </summary>
        public ContentTypeAttribute ContentType { get { return _contentTypeAttribute; } }

        /// <summary>
        /// Object width.
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; } }

        /// <summary>
        /// This attribute may be used to specify a space-separated list of URIs for archives containing resources relevant to the object.
        /// </summary>
        public ArchiveAttribute Archive { get { return _archiveAttribute; } }

        /// <summary>
        /// This attribute specifies the content type of data expected when downloading the object specified by classid.
        /// </summary>
        public CodeTypeAttribute CodeType { get { return _codeTypeAttribute; } }

        /// <summary>
        /// This attribute may be used to specify the location of the object's data, 
        /// for instance image data for objects defining images, 
        /// or more generally, a serialized form of an object which can be used to recreate it.
        /// </summary>
        public DataAttribute Data { get { return _dataAttribute; } }

        /// <summary>
        /// When present, this attribute makes the current object definition a declaration only. 
        /// This means another object element will be used to load the object. 
        /// Possible value is "declare".
        /// </summary>
        public DeclareAttribute Declare { get { return _declareAttribute; } }


        /// <summary>
        /// This attribute specifies a message that a Web browser may display while loading the object
        /// </summary>
        public StandByAttribute Standby { get { return _standbyAttribute; } }

        /// <summary>
        /// Position in tabbing order.
        /// </summary>
        public TabIndexAttribute TabIndex { get { return _tabIndexAttribute; } }

        /// <summary>
        /// This attribute associates the object to a client-side image map defined by a map element. 
        /// The value of this attribute must match the id attribute of the map element.
        /// </summary>
        public UseMapAttribute UseMap { get { return _useMapAttribute; } }



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

            _classIdAttribute.ReadAttribute(xElement);
            _codeBaseAttribute.ReadAttribute(xElement);
            _heightAttribute.ReadAttribute(xElement);
            _nameAttribute.ReadAttribute(xElement);
            _contentTypeAttribute.ReadAttribute(xElement);
            _widthAttribute.ReadAttribute(xElement);

            _archiveAttribute.ReadAttribute(xElement);
            _codeTypeAttribute.ReadAttribute(xElement);
            _dataAttribute.ReadAttribute(xElement);
            _declareAttribute.ReadAttribute(xElement);
            _standbyAttribute.ReadAttribute(xElement);
            _tabIndexAttribute.ReadAttribute(xElement);
            _useMapAttribute.ReadAttribute(xElement);

            _language.ReadAttribute(xElement);
            _direction.ReadAttribute(xElement);

            _onClick.ReadAttribute(xElement);
            _onDblClick.ReadAttribute(xElement);
            _onMouseDown.ReadAttribute(xElement);
            _onMouseUp.ReadAttribute(xElement);
            _onMouseOver.ReadAttribute(xElement);
            _onMouseMove.ReadAttribute(xElement);
            _onMouseOut.ReadAttribute(xElement);
            _onKeyPress.ReadAttribute(xElement);
            _onKeyDown.ReadAttribute(xElement);
            _onKeyUp.ReadAttribute(xElement);

            _content.Clear();
            IEnumerable<XNode> descendants = xElement.Nodes();
            foreach (var node in descendants)
            {
                IHTML5Item item = ElementFactory.CreateElement(node);
                if ((item != null) && IsValidSubType(item))
                {
                    try
                    {
                        item.Load(node);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    _content.Add(item);
                }
            }

        }

        private bool IsValidSubType(IHTML5Item item)
        {
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            if (item is Param)
            {
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            _classIdAttribute.AddAttribute(xElement);
            _codeBaseAttribute.AddAttribute(xElement);
            _heightAttribute.AddAttribute(xElement);
            _nameAttribute.AddAttribute(xElement);
            _contentTypeAttribute.AddAttribute(xElement);
            _widthAttribute.AddAttribute(xElement);

            _archiveAttribute.AddAttribute(xElement);
            _codeTypeAttribute.AddAttribute(xElement);
            _dataAttribute.AddAttribute(xElement);
            _declareAttribute.AddAttribute(xElement);
            _standbyAttribute.AddAttribute(xElement);
            _tabIndexAttribute.ReadAttribute(xElement);
            _useMapAttribute.ReadAttribute(xElement);

            _language.AddAttribute(xElement);
            _direction.AddAttribute(xElement);

            _onClick.AddAttribute(xElement);
            _onDblClick.AddAttribute(xElement);
            _onMouseDown.AddAttribute(xElement);
            _onMouseUp.AddAttribute(xElement);
            _onMouseOver.AddAttribute(xElement);
            _onMouseMove.AddAttribute(xElement);
            _onMouseOut.AddAttribute(xElement);
            _onKeyPress.AddAttribute(xElement);
            _onKeyDown.AddAttribute(xElement);
            _onKeyUp.AddAttribute(xElement);

            foreach (var item in _content)
            {
                xElement.Add(item.Generate());
            }
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
            if ((item != null) && IsValidSubType(item))
            {
                _content.Add(item);
                item.Parent = this;
            }
            else
            {
                throw new HTML5ViolationException();
            }
        }

        public override void Remove(IHTML5Item item)
        {
            if(_content.Remove(item))
            {
                item.Parent = null;
            }
        }

        public override List<IHTML5Item> SubElements()
        {
            return _content;
        }
    }
}
