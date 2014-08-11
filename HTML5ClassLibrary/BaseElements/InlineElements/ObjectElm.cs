using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
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


        private readonly HeightAttribute _heightAttribute = new HeightAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly ContentTypeAttribute _contentTypeAttribute = new ContentTypeAttribute();
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly DataAttribute _dataAttribute = new DataAttribute();
        private readonly MIMETypeAttribute _mimeTypeAttribute= new MIMETypeAttribute();
        private readonly UseMapAttribute _useMapAttribute = new UseMapAttribute();


        public const string ElementName = "object";



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
        /// Specifies one or more forms the object belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// This attribute may be used to specify the location of the object's data, 
        /// for instance image data for objects defining images, 
        /// or more generally, a serialized form of an object which can be used to recreate it.
        /// </summary>
        public DataAttribute Data { get { return _dataAttribute; } }


        /// <summary>
        /// Specifies the MIME type of data specified in the data attribute
        /// </summary>
        public MIMETypeAttribute TabIndex { get { return _mimeTypeAttribute; } }

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

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _heightAttribute.AddAttribute(xElement);
            _nameAttribute.AddAttribute(xElement);
            _contentTypeAttribute.AddAttribute(xElement);
            _widthAttribute.AddAttribute(xElement);
            _formIdAttribute.AddAttribute(xElement);
            _dataAttribute.AddAttribute(xElement);
            _mimeTypeAttribute.ReadAttribute(xElement);
            _useMapAttribute.ReadAttribute(xElement);
       }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _heightAttribute.ReadAttribute(xElement);
            _nameAttribute.ReadAttribute(xElement);
            _contentTypeAttribute.ReadAttribute(xElement);
            _widthAttribute.ReadAttribute(xElement);
            _formIdAttribute.ReadAttribute(xElement);
            _dataAttribute.ReadAttribute(xElement);
            _mimeTypeAttribute.ReadAttribute(xElement);
            _useMapAttribute.ReadAttribute(xElement);
        }
    }
}
