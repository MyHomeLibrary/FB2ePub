using System.Collections.Generic;
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
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly FrameBorderAttribute _frameBorderAttribute = new FrameBorderAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly LongDescriptionAttribute _longDescriptionAttribute = new LongDescriptionAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly MarginHeightAttribute _marginHeightAttribute = new MarginHeightAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly MarginWidthAttribute _marginWidthAttribute = new MarginWidthAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly NoResizeAttribute _noResizeAttribute = new NoResizeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly ScrollingAttribute _scrollingAttribute = new ScrollingAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly SourceAttribute _sourceAttribute = new SourceAttribute();





        /// <summary>
        /// Specifies whether or not to display a border around a frame
        /// Not supported in HTML5.
        /// </summary>
        public FrameBorderAttribute FrameBorder { get { return _frameBorderAttribute; }}


        /// <summary>
        /// Specifies a page that contains a long description of the content of a frame
        /// Not supported in HTML5.
        /// </summary>
        public LongDescriptionAttribute LongDescription { get { return _longDescriptionAttribute; }}


        /// <summary>
        /// Specifies the top and bottom margins of a frame
        /// Not supported in HTML5.
        /// </summary>
        public MarginHeightAttribute MarginHeight { get { return _marginHeightAttribute; }}


        /// <summary>
        /// Specifies the left and right margins of a frame
        /// Not supported in HTML5.
        /// </summary>
        public MarginWidthAttribute MarginWidth { get { return _marginWidthAttribute; }}


        /// <summary>
        /// Specifies the name of a frame
        /// Not supported in HTML5.
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}


        /// <summary>
        /// Specifies that a frame is not resizable
        /// Not supported in HTML5.
        /// </summary>
        public NoResizeAttribute NoResize { get { return _noResizeAttribute; }}


        /// <summary>
        /// Specifies whether or not to display scrollbars in a frame
        /// Not supported in HTML5.
        /// </summary>
        public ScrollingAttribute Scrolling { get { return _scrollingAttribute; }}


        /// <summary>
        /// Specifies the URL of the document to show in a frame
        /// Not supported in HTML5.
        /// </summary>
        public SourceAttribute Src { get { return _sourceAttribute; }}




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
