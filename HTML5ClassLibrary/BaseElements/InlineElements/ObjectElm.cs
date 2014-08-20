using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements.BlockElements;
using XHTMLClassLibrary.BaseElements.ObjectParameters;
using XHTMLClassLibrary.Exceptions;

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
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly AlignAttribute _alignAttribute = new AlignAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ArchiveAttribute _archiveAttribute = new ArchiveAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BorderAttribute _borderAttribute = new BorderAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ClassIDAttribute _classIDAttribute = new ClassIDAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CodeBaseAttribute _codeBaseAttribute = new CodeBaseAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CodeTypeAttribute _codeTypeAttribute = new CodeTypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly DataAttribute _dataAttribute = new DataAttribute();

        [AttributeTypeAttributeMember(Name = "declare", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _declareAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HSpaceAttribute _hSpaceAttribute = new HSpaceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly StandByAttribute _standByAttribute = new StandByAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly MIMETypeAttribute _mimeTypeAttribute = new MIMETypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly UseMapAttribute _useMapAttribute = new UseMapAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VSpaceAttribute _vSpaceAttribute = new VSpaceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();





        /// <summary>
        ///  Specifies the alignment of the "object" element according to surrounding elements
        /// Not supported in HTML5.
        /// </summary>
        public AlignAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        /// A space separated list of URL's to archives. The archives contains resources relevant to the object
        /// Not supported in HTML5.
        /// </summary>
        public ArchiveAttribute Archive { get { return _archiveAttribute; }}


        /// <summary>
        ///  Specifies the width of the border around an <object>
        /// Not supported in HTML5.
        /// </summary>
        public BorderAttribute Border { get { return _borderAttribute; }}


        /// <summary>
        /// Defines a class ID value as set in the Windows Registry or a URL
        /// Not supported in HTML5.
        /// </summary>
        public ClassIDAttribute ClassID { get { return _classIDAttribute; }}


        /// <summary>
        /// Defines where to find the code for the object
        /// Not supported in HTML5.
        /// </summary>
        public CodeBaseAttribute CodeBase { get { return _codeBaseAttribute; }}


        /// <summary>
        /// The media type of the code referred to by the classid attribute
        /// Not supported in HTML5.
        /// </summary>
        public CodeTypeAttribute CodeType { get { return _codeTypeAttribute; }}

        /// <summary>
        /// Object height.
        /// </summary>
        public HeightAttribute Height { get { return _heightAttribute; } }


        /// <summary>
        ///  Specifies the whitespace on left and right side of an object
        /// Not supported in HTML5.
        /// </summary>
        public HSpaceAttribute HSpace { get { return _hSpaceAttribute; }}

        /// <summary>
        /// When the object is used as a form control, this attribute is the name of the form control.
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; } }


        /// <summary>
        /// Defines a text to display while the object is loading
        /// Not supported in HTML5.
        /// </summary>
        public StandByAttribute StandBy { get { return _standByAttribute; }}

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
        /// Defines that the object should only be declared, not created or instantiated until needed
        /// Not supported in HTML5.
        /// </summary>
        public FlagAttribute Declare { get { return _declareAttribute; }}


        /// <summary>
        /// Specifies the MIME type of data specified in the data attribute
        /// </summary>
        public MIMETypeAttribute TabIndex { get { return _mimeTypeAttribute; } }

        /// <summary>
        /// This attribute associates the object to a client-side image map defined by a map element. 
        /// The value of this attribute must match the id attribute of the map element.
        /// </summary>
        public UseMapAttribute UseMap { get { return _useMapAttribute; } }


        /// <summary>
        ///  Specifies the whitespace on top and bottom of an object
        /// Not supported in HTML5.
        /// </summary>
        public VSpaceAttribute VSpace { get { return _vSpaceAttribute; }}


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
