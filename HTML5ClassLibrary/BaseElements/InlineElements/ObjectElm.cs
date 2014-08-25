using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.ObjectParameters;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The object element provides a generic way of embedding objects such as images, 
    /// movies and applications (Java applets, browser plug-ins, etc.) into Web pages. 
    /// "param" elements contained inside the object element are used to configure the embedded object. 
    /// Besides "param" elements, the object element can contain alternate content which can be text or another object element. 
    /// Alternate content serves as a fall-back mechanism for browsers that are unable to process the embedded object.
    /// </summary>
    [HTMLItemAttribute(ElementName = "object", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]    
    public class ObjectElm : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("center;justify;right;left;char");

        [AttributeTypeAttributeMember(Name = "archive", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URIsTypeAttribute _archiveAttribute = new URIsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "border", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly PixelsTypeAttribute _borderAttribute = new PixelsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "classid", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _classIDAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "codebase", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _codeBaseAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "codetype", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ContentTypeAttribute _codeTypeAttribute = new ContentTypeAttribute();

        [AttributeTypeAttributeMember(Name = "data", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _dataAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "declare", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _declareAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "form", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _formIdAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _heightAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "hspace", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _hSpaceAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _nameAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "standby", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _standByAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly MIMETypeAttribute _mimeTypeAttribute = new MIMETypeAttribute();

        [AttributeTypeAttributeMember(Name = "usemap", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly IdReferenceTypeAttribute _useMapAttribute = new IdReferenceTypeAttribute();

        [AttributeTypeAttributeMember(Name = "vspace", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _vSpaceAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _widthAttribute = new LengthTypeAttribute();





        /// <summary>
        ///  Specifies the alignment of the "object" element according to surrounding elements
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }


        /// <summary>
        /// A space separated list of URL's to archives. The archives contains resources relevant to the object
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Archive { get { return _archiveAttribute; } }


        /// <summary>
        ///  Specifies the width of the border around an <object>
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Border { get { return _borderAttribute; } }


        /// <summary>
        /// Defines a class ID value as set in the Windows Registry or a URL
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess ClassID { get { return _classIDAttribute; } }


        /// <summary>
        /// Defines where to find the code for the object
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess CodeBase { get { return _codeBaseAttribute; } }


        /// <summary>
        /// The media type of the code referred to by the classid attribute
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess CodeType { get { return _codeTypeAttribute; } }

        /// <summary>
        /// Object height.
        /// </summary>
        public IAttributeDataAccess Height { get { return _heightAttribute; } }


        /// <summary>
        ///  Specifies the whitespace on left and right side of an object
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess HSpace { get { return _hSpaceAttribute; } }

        /// <summary>
        /// When the object is used as a form control, this attribute is the name of the form control.
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }


        /// <summary>
        /// Defines a text to display while the object is loading
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess StandBy { get { return _standByAttribute; } }

        /// <summary>
        /// Object width.
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; } }

        /// <summary>
        /// Specifies one or more forms the object belongs to
        /// </summary>
        public IAttributeDataAccess Form { get { return _formIdAttribute; } }

        /// <summary>
        /// This attribute may be used to specify the location of the object's data, 
        /// for instance image data for objects defining images, 
        /// or more generally, a serialized form of an object which can be used to recreate it.
        /// </summary>
        public IAttributeDataAccess Data { get { return _dataAttribute; } }


        /// <summary>
        /// Defines that the object should only be declared, not created or instantiated until needed
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Declare { get { return _declareAttribute; } }


        /// <summary>
        /// Specifies the MIME type of data specified in the data attribute
        /// </summary>
        public IAttributeDataAccess TabIndex { get { return _mimeTypeAttribute; } }

        /// <summary>
        /// This attribute associates the object to a client-side image map defined by a map element. 
        /// The value of this attribute must match the id attribute of the map element.
        /// </summary>
        public IAttributeDataAccess UseMap { get { return _useMapAttribute; } }


        /// <summary>
        ///  Specifies the whitespace on top and bottom of an object
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess VSpace { get { return _vSpaceAttribute; } }


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
