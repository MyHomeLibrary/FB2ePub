using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.BaseElements.BlockElements;
using HTMLClassLibrary.BaseElements.ObjectParameters;
using HTMLClassLibrary.Exceptions;

namespace HTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The object element provides a generic way of embedding objects such as images, 
    /// movies and applications (Java applets, browser plug-ins, etc.) into Web pages. 
    /// "param" elements contained inside the object element are used to configure the embedded object. 
    /// Besides "param" elements, the object element can contain alternate content which can be text or another object element. 
    /// Alternate content serves as a fall-back mechanism for browsers that are unable to process the embedded object.
    /// </summary>
    [HTMLItemAttribute(ElementName = "object", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]    
    public class ObjectElm : HTMLItem, IInlineItem
    {
        public ObjectElm()
        {
            RegisterAttribute(_heightAttribute);
            RegisterAttribute(_nameAttribute);
            RegisterAttribute(_contentTypeAttribute);
            RegisterAttribute(_widthAttribute);
            RegisterAttribute(_formIdAttribute);
            RegisterAttribute(_dataAttribute);
            RegisterAttribute(_mimeTypeAttribute);
            RegisterAttribute(_useMapAttribute);
           
        }


        private readonly HeightAttribute _heightAttribute = new HeightAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly ContentTypeAttribute _contentTypeAttribute = new ContentTypeAttribute();
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly DataAttribute _dataAttribute = new DataAttribute();
        private readonly MIMETypeAttribute _mimeTypeAttribute= new MIMETypeAttribute();
        private readonly UseMapAttribute _useMapAttribute = new UseMapAttribute();


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


        protected override bool IsValidSubType(IHTMLItem item)
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

        public override bool IsValid()
        {
            return true;
        }
    }
}
