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

        [AttributeTypeAttributeMember(Name = "longdesc", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _longDescriptionAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "marginheight", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly PixelsTypeAttribute _marginHeightAttribute = new PixelsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "marginwidth", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly PixelsTypeAttribute _marginWidthAttribute = new PixelsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _nameAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly NoResizeAttribute _noResizeAttribute = new NoResizeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.FrameSet)]
        private readonly ScrollingAttribute _scrollingAttribute = new ScrollingAttribute();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _sourceAttribute = new URITypeAttribute();





        /// <summary>
        /// Specifies whether or not to display a border around a frame
        /// Not supported in HTML5.
        /// </summary>
        public FrameBorderAttribute FrameBorder { get { return _frameBorderAttribute; }}


        /// <summary>
        /// Specifies a page that contains a long description of the content of a frame
        /// Not supported in HTML5.
        /// </summary>
        public URITypeAttribute LongDescription { get { return _longDescriptionAttribute; }}


        /// <summary>
        /// Specifies the top and bottom margins of a frame
        /// Not supported in HTML5.
        /// </summary>
        public PixelsTypeAttribute MarginHeight { get { return _marginHeightAttribute; }}


        /// <summary>
        /// Specifies the left and right margins of a frame
        /// Not supported in HTML5.
        /// </summary>
        public PixelsTypeAttribute MarginWidth { get { return _marginWidthAttribute; }}


        /// <summary>
        /// Specifies the name of a frame
        /// Not supported in HTML5.
        /// </summary>
        public TextValueAttribute Name { get { return _nameAttribute; }}


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
        public URITypeAttribute Src { get { return _sourceAttribute; }}




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
