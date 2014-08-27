using System.Collections.Generic;
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
        [AttributeTypeAttributeMember(Name = "frameborder", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _frameBorderAttribute = new ValuesSelectionTypeAttribute<Text>("0;1");

        [AttributeTypeAttributeMember(Name = "longdesc", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _longDescriptionAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "marginheight", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Pixels> _marginHeightAttribute = new SimpleSingleTypeAttribute<Pixels>();

        [AttributeTypeAttributeMember(Name = "marginwidth", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Pixels> _marginWidthAttribute = new SimpleSingleTypeAttribute<Pixels>();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _nameAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "noresize", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _noResizeAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "scrolling", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _scrollingAttribute = new ValuesSelectionTypeAttribute<Text>("auto;yes;no");

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _sourceAttribute = new SimpleSingleTypeAttribute<URI>();





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
