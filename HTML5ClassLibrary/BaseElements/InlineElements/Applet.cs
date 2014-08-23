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
        [AttributeTypeAttributeMember(Name = "code", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _codeAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "object", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _objectAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignTypeAttribute _alignAttribute = new BiDirectionalAlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "alt", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _altAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "archive", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly URIsTypeAttribute _archiveAttribute = new URIsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "codebase", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _codeBaseAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _height = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "hspace", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _hSpaceAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _nameAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "vspace", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _vSpaceAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _widthAttribute = new LengthTypeAttribute();





        /// <summary>
        /// Specifies the width of an applet
        /// </summary>
        public LengthTypeAttribute Width { get { return _widthAttribute; }}


        /// <summary>
        /// Defines the vertical spacing around an applet
        /// </summary>
        public LengthTypeAttribute VSpace { get { return _vSpaceAttribute; }}
           

        /// <summary>
        /// Defines the name for an applet (to use in scripts)
        /// </summary>
        public TextValueTypeAttribute Name { get { return _nameAttribute; }}

        /// <summary>
        /// Defines the horizontal spacing around an applet
        /// </summary>
        public LengthTypeAttribute HSpace { get { return _hSpaceAttribute; }}

        /// <summary>
        /// Specifies the height of an applet
        /// </summary>
        public LengthTypeAttribute Height { get { return _height; }}

        /// <summary>
        /// Specifies a relative base URL for applets specified in the code attribute
        /// </summary>
        public URITypeAttribute CodeBase { get { return _codeBaseAttribute; }}

        /// <summary>
        /// Specifies the location of an archive file
        /// </summary>
        public URIsTypeAttribute Archive { get { return _archiveAttribute; }}

        /// <summary>
        /// Specifies an alternate text for an applet
        /// </summary>
        public TextValueTypeAttribute Alt { get { return _altAttribute; }}

        /// <summary>
        /// Specifies the alignment of an applet according to surrounding elements
        /// </summary>
        public BiDirectionalAlignTypeAttribute Align { get { return _alignAttribute; }}

        /// <summary>
        /// Specifies the file name of a Java applet
        /// </summary>
        public URITypeAttribute Code { get { return _codeAttribute; }}

        /// <summary>
        /// Specifies a reference to a serialized representation of an applet
        /// </summary>
        public TextValueTypeAttribute Object { get { return _objectAttribute; }}


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
