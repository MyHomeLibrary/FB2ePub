using System.Collections.Generic;
using System.ComponentModel;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.Frameset
{
    /// <summary>
    /// The "frame" tag defines one particular window (frame) within a "frameset".
    /// Each "frame" in a "frameset" can have different attributes, such as border, scrolling, the ability to resize, etc.
    /// Note: If you want to validate a page containing frames, be sure the "!DOCTYPE" is set to either "HTML Frameset DTD" or "XHTML Frameset DTD".
    /// </summary>
    [HTMLItem(ElementName = "frame", SupportedStandards = HTMLElementType.FrameSet)]
    public class Frame : HTMLItem, IInlineItem
    {

#region Attribute_Values_Enums

        /// <summary>
        /// "frameborder" attribute possible values
        /// </summary>
        public enum FrameBorderAttributeOptions
        {
            [Description("0")]
            Disabled,

            [Description("1")]
            Enabled,
        }


        /// <summary>
        /// "scrolling" attribute possible values
        /// </summary>
        public enum ScrollingAttributeOptions
        {
            [Description("auto")]
            Auto,

            [Description("yes")]
            Yes,

            [Description("no")]
            No,
        }

#endregion


        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _frameBorderAttribute = new ValuesSelectionTypeAttribute<Text>("frameborder",typeof(FrameBorderAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _longDescriptionAttribute = new SimpleSingleTypeAttribute<URI>("longdesc");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Pixels> _marginHeightAttribute = new SimpleSingleTypeAttribute<Pixels>("marginheight");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Pixels> _marginWidthAttribute = new SimpleSingleTypeAttribute<Pixels>("marginwidth");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>("name");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _noResizeAttribute = new FlagTypeAttribute("noresize");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _scrollingAttribute = new ValuesSelectionTypeAttribute<Text>( "scrolling",typeof(ScrollingAttributeOptions));

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _sourceAttribute = new SimpleSingleTypeAttribute<URI>("src");





        /// <summary>
        /// Specifies whether or not to display a border around a frame
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess FrameBorder { get { return _frameBorderAttribute; }}


        /// <summary>
        /// Specifies a page that contains a long description of the content of a frame
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess LongDescription { get { return _longDescriptionAttribute; } }


        /// <summary>
        /// Specifies the top and bottom margins of a frame
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess MarginHeight { get { return _marginHeightAttribute; } }


        /// <summary>
        /// Specifies the left and right margins of a frame
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess MarginWidth { get { return _marginWidthAttribute; } }


        /// <summary>
        /// Specifies the name of a frame
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Name { get { return _nameAttribute; } }


        /// <summary>
        /// Specifies that a frame is not resizable
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess NoResize { get { return _noResizeAttribute; } }


        /// <summary>
        /// Specifies whether or not to display scrollbars in a frame
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Scrolling { get { return _scrollingAttribute; } }


        /// <summary>
        /// Specifies the URL of the document to show in a frame
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Src { get { return _sourceAttribute; } }




        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }

    }
}
