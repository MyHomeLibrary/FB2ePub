using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "applet" tag defines an embedded applet.
    /// </summary>
    [HTMLItemAttribute(ElementName = "applet", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class Applet : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly CodeAttribute _codeAttribute = new CodeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly ObjectAttribute _objectAttribute = new ObjectAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignAttribute _alignAttribute = new BiDirectionalAlignAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly AltAttribute _altAttribute = new AltAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly ArchiveAttribute _archiveAttribute = new ArchiveAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly CodeBaseAttribute _codeBaseAttribute = new CodeBaseAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly HeightAttribute _height = new HeightAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly HSpaceAttribute _hSpaceAttribute = new HSpaceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly VSpaceAttribute _vSpaceAttribute = new VSpaceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();





        /// <summary>
        /// Specifies the width of an applet
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; }}


        /// <summary>
        /// Defines the vertical spacing around an applet
        /// </summary>
        public VSpaceAttribute VSpace { get { return _vSpaceAttribute; }}
           

        /// <summary>
        /// Defines the name for an applet (to use in scripts)
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}

        /// <summary>
        /// Defines the horizontal spacing around an applet
        /// </summary>
        public HSpaceAttribute HSpace { get { return _hSpaceAttribute; }}

        /// <summary>
        /// Specifies the height of an applet
        /// </summary>
        public HeightAttribute Height { get { return _height; }}

        /// <summary>
        /// Specifies a relative base URL for applets specified in the code attribute
        /// </summary>
        public CodeBaseAttribute CodeBase { get { return _codeBaseAttribute; }}

        /// <summary>
        /// Specifies the location of an archive file
        /// </summary>
        public ArchiveAttribute Archive { get { return _archiveAttribute; }}

        /// <summary>
        /// Specifies an alternate text for an applet
        /// </summary>
        public AltAttribute Alt { get { return _altAttribute; }}

        /// <summary>
        /// Specifies the alignment of an applet according to surrounding elements
        /// </summary>
        public BiDirectionalAlignAttribute Align { get { return _alignAttribute; }}

        /// <summary>
        /// Specifies the file name of a Java applet
        /// </summary>
        public CodeAttribute Code { get { return _codeAttribute; }}

        /// <summary>
        /// Specifies a reference to a serialized representation of an applet
        /// </summary>
        public ObjectAttribute Object { get { return _objectAttribute; }}


        public override bool IsValid()
        {
            return (_codeAttribute.HasValue() && _objectAttribute.HasValue());
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
