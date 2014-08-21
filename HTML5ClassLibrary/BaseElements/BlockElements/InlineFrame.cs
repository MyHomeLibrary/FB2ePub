using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    [HTMLItemAttribute(ElementName = "iframe", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class InlineFrame : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignTypeAttribute _alignAttribute = new BiDirectionalAlignTypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FrameBorderAttribute _frameBorderAttribute = new FrameBorderAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _heightAttribute = new LengthAttribute();

        [AttributeTypeAttributeMember(Name = "longdesc", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _longDescriptionAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "marginheight", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly PixelsTypeAttribute _marginHeightAttribute = new PixelsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "marginwidth", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly PixelsTypeAttribute _marginWidthAttribute = new PixelsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _nameAttribute = new TextValueAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SandboxAttribute _sandboxAttribute = new SandboxAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ScrollingAttribute _scrollingAttribute = new ScrollingAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SeamlessAttribute _seamlessAttribute = new SeamlessAttribute();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _sourceAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "srcdoc", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly HTMLCodeAttribute _sourceDocAttribute = new HTMLCodeAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthAttribute _widthAttribute = new LengthAttribute();

        




        /// <summary>
        ///  Specifies the alignment of an "iframe" according to surrounding elements
        /// Not supported in HTML5.
        /// </summary>
        public BiDirectionalAlignTypeAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        /// Specifies whether or not to display a border around an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public FrameBorderAttribute FrameBorder { get { return _frameBorderAttribute; }}


        /// <summary>
        /// Specifies the height of an "iframe"
        /// </summary>
        public LengthAttribute Height { get { return _heightAttribute; }}


        /// <summary>
        /// Specifies a page that contains a long description of the content of an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public URITypeAttribute LongDescription { get { return _longDescriptionAttribute; }}


        /// <summary>
        /// Specifies the top and bottom margins of the content of an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public PixelsTypeAttribute MarginHeight { get { return _marginHeightAttribute; }}


        /// <summary>
        /// Specifies the left and right margins of the content of an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public PixelsTypeAttribute MarginWidth { get { return _marginWidthAttribute; }}

        /// <summary>
        /// Specifies the width of an "iframe"
        /// </summary>
        public LengthAttribute Width { get { return _widthAttribute; }}

        /// <summary>
        /// Specifies the address of the document to embed in the "iframe"
        /// </summary>
        public URITypeAttribute Src { get { return _sourceAttribute; }}

        /// <summary>
        /// Specifies the name of an "iframe"
        /// </summary>
        public TextValueAttribute Name { get { return _nameAttribute; }}

        /// <summary>
        /// Enables a set of extra restrictions for the content in the "iframe"
        /// </summary>
        public SandboxAttribute Sandbox { get { return _sandboxAttribute; }}


        /// <summary>
        /// Specifies whether or not to display scrollbars in an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public ScrollingAttribute Scrolling { get { return _scrollingAttribute; }}

        /// <summary>
        /// Specifies that the "iframe" should look like it is a part of the containing document
        /// </summary>
        public SeamlessAttribute Seamless { get { return _seamlessAttribute; }}

        /// <summary>
        /// Specifies the HTML content of the page to show in the "iframe"
        /// </summary>
        public HTMLCodeAttribute SrcDoc { get { return _sourceDocAttribute; }}


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
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
