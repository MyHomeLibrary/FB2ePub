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
        private readonly TextValueAttribute _objectAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignTypeAttribute _alignAttribute = new BiDirectionalAlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "alt", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _altAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(Name = "archive", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly URIsTypeAttribute _archiveAttribute = new URIsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "codebase", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _codeBaseAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _height = new LengthAttribute();

        [AttributeTypeAttributeMember(Name = "hspace", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _hSpaceAttribute = new LengthAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _nameAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(Name = "vspace", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _vSpaceAttribute = new LengthAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _widthAttribute = new LengthAttribute();





        /// <summary>
        /// Specifies the width of an applet
        /// </summary>
        public LengthAttribute Width { get { return _widthAttribute; }}


        /// <summary>
        /// Defines the vertical spacing around an applet
        /// </summary>
        public LengthAttribute VSpace { get { return _vSpaceAttribute; }}
           

        /// <summary>
        /// Defines the name for an applet (to use in scripts)
        /// </summary>
        public TextValueAttribute Name { get { return _nameAttribute; }}

        /// <summary>
        /// Defines the horizontal spacing around an applet
        /// </summary>
        public LengthAttribute HSpace { get { return _hSpaceAttribute; }}

        /// <summary>
        /// Specifies the height of an applet
        /// </summary>
        public LengthAttribute Height { get { return _height; }}

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
        public TextValueAttribute Alt { get { return _altAttribute; }}

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
        public TextValueAttribute Object { get { return _objectAttribute; }}


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
