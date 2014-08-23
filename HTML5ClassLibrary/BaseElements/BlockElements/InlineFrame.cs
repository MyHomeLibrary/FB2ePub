using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    [HTMLItemAttribute(ElementName = "iframe", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class InlineFrame : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignTypeAttribute _alignAttribute = new BiDirectionalAlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "frameborder", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FrameBorderAttribute _frameBorderAttribute = new FrameBorderAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _heightAttribute = new LengthTypeAttribute();

        [AttributeTypeAttributeMember(Name = "longdesc", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _longDescriptionAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "marginheight", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly PixelsTypeAttribute _marginHeightAttribute = new PixelsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "marginwidth", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly PixelsTypeAttribute _marginWidthAttribute = new PixelsTypeAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _nameAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "sandbox", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SandboxTypeAttribute _sandboxAttribute = new SandboxTypeAttribute();

        [AttributeTypeAttributeMember(Name = "scrolling", SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly YesNoAutoTypeAttribute _scrollingAttribute = new YesNoAutoTypeAttribute();

        [AttributeTypeAttributeMember(Name = "seamless", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _seamlessAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _sourceAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "srcdoc", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly HTMLCodeTypeAttribute _sourceDocAttribute = new HTMLCodeTypeAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LengthTypeAttribute _widthAttribute = new LengthTypeAttribute();

        




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
        public LengthTypeAttribute Height { get { return _heightAttribute; }}


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
        public LengthTypeAttribute Width { get { return _widthAttribute; }}

        /// <summary>
        /// Specifies the address of the document to embed in the "iframe"
        /// </summary>
        public URITypeAttribute Src { get { return _sourceAttribute; }}

        /// <summary>
        /// Specifies the name of an "iframe"
        /// </summary>
        public TextValueTypeAttribute Name { get { return _nameAttribute; }}

        /// <summary>
        /// Enables a set of extra restrictions for the content in the "iframe"
        /// </summary>
        public SandboxTypeAttribute Sandbox { get { return _sandboxAttribute; }}


        /// <summary>
        /// Specifies whether or not to display scrollbars in an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public YesNoAutoTypeAttribute Scrolling { get { return _scrollingAttribute; }}

        /// <summary>
        /// Specifies that the "iframe" should look like it is a part of the containing document
        /// </summary>
        public FlagTypeAttribute Seamless { get { return _seamlessAttribute; }}

        /// <summary>
        /// Specifies the HTML content of the page to show in the "iframe"
        /// </summary>
        public HTMLCodeTypeAttribute SrcDoc { get { return _sourceDocAttribute; }}


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
