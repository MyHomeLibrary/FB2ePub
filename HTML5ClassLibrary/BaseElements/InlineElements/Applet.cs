using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "applet" tag defines an embedded applet.
    /// </summary>
    [HTMLItemAttribute(ElementName = "applet", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class Applet : HTMLItem, IInlineItem
    {

        #region Attribute_Values_Enums

        /// <summary>
        /// "align" attribute possible values
        /// </summary>
        public enum AlignAttributeOptions
        {
            [Description("baseline")]
            Baseline,

            [Description("bottom")]
            Bottom,

            [Description("left")]
            Left,

            [Description("middle")]
            Middle,

            [Description("right")]
            Right,

            [Description("top")]
            Top,

        }

        #endregion 


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _codeAttribute = new SimpleSingleTypeAttribute<URI>("code");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _objectAttribute = new SimpleSingleTypeAttribute<Text>("object");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _alignAttribute = new ValuesSelectionTypeAttribute<Text>("align",typeof(AlignAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _altAttribute = new SimpleSingleTypeAttribute<Text>("alt");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URIs> _archiveAttribute = new SimpleSingleTypeAttribute<URIs>("archive");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _codeBaseAttribute = new SimpleSingleTypeAttribute<URI>("codebase");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _height = new SimpleSingleTypeAttribute<Length>("height");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _hSpaceAttribute = new SimpleSingleTypeAttribute<Length>("hspace");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>("name");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _vSpaceAttribute = new SimpleSingleTypeAttribute<Length>("vspace");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Length> _widthAttribute = new SimpleSingleTypeAttribute<Length>("width");


        public Applet(HTMLElementType htmlStandard) : base(htmlStandard)
        {
        }

        /// <summary>
        /// Specifies the width of an applet
        /// </summary>
        public IAttributeDataAccess Width { get { return _widthAttribute; }}


        /// <summary>
        /// Defines the vertical spacing around an applet
        /// </summary>
        public IAttributeDataAccess VSpace { get { return _vSpaceAttribute; } }
           

        /// <summary>
        /// Defines the name for an applet (to use in scripts)
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }

        /// <summary>
        /// Defines the horizontal spacing around an applet
        /// </summary>
        public IAttributeDataAccess HSpace { get { return _hSpaceAttribute; } }

        /// <summary>
        /// Specifies the height of an applet
        /// </summary>
        public IAttributeDataAccess Height { get { return _height; } }

        /// <summary>
        /// Specifies a relative base URL for applets specified in the code attribute
        /// </summary>
        public IAttributeDataAccess CodeBase { get { return _codeBaseAttribute; } }

        /// <summary>
        /// Specifies the location of an archive file
        /// </summary>
        public IAttributeDataAccess Archive { get { return _archiveAttribute; } }

        /// <summary>
        /// Specifies an alternate text for an applet
        /// </summary>
        public IAttributeDataAccess Alt { get { return _altAttribute; } }

        /// <summary>
        /// Specifies the alignment of an applet according to surrounding elements
        /// </summary>
        public IAttributeDataAccess Align { get { return _alignAttribute; } }

        /// <summary>
        /// Specifies the file name of a Java applet
        /// </summary>
        public IAttributeDataAccess Code { get { return _codeAttribute; } }

        /// <summary>
        /// Specifies a reference to a serialized representation of an applet
        /// </summary>
        public IAttributeDataAccess Object { get { return _objectAttribute; } }


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
