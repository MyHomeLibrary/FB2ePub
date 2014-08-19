using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    [HTMLItemAttribute(ElementName = "iframe", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.FrameSet)]
    public class InlineFrame : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignAttribute _alignAttribute = new BiDirectionalAlignAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FrameBorderAttribute _frameBorderAttribute = new FrameBorderAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5| HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LongDescriptionAttribute _longDescriptionAttribute = new LongDescriptionAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly MarginHeightAttribute _marginHeightAttribute = new MarginHeightAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly MarginWidthAttribute _marginWidthAttribute = new MarginWidthAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SandboxAttribute _sandboxAttribute = new SandboxAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ScrollingAttribute _scrollingAttribute = new ScrollingAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SeamlessAttribute _seamlessAttribute = new SeamlessAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SourceAttribute _sourceAttribute = new SourceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SourceDocAttribute _sourceDocAttribute = new SourceDocAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();

        




        /// <summary>
        ///  Specifies the alignment of an "iframe" according to surrounding elements
        /// Not supported in HTML5.
        /// </summary>
        public BiDirectionalAlignAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        /// Specifies whether or not to display a border around an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public FrameBorderAttribute FrameBorder { get { return _frameBorderAttribute; }}


        /// <summary>
        /// Specifies the height of an "iframe"
        /// </summary>
        public HeightAttribute Height { get { return _heightAttribute; }}


        /// <summary>
        /// Specifies a page that contains a long description of the content of an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public LongDescriptionAttribute LongDescription { get { return _longDescriptionAttribute; }}


        /// <summary>
        /// Specifies the top and bottom margins of the content of an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public MarginHeightAttribute MarginHeight { get { return _marginHeightAttribute; }}


        /// <summary>
        /// Specifies the left and right margins of the content of an "iframe"
        /// Not supported in HTML5.
        /// </summary>
        public MarginWidthAttribute MarginWidth { get { return _marginWidthAttribute; }}

        /// <summary>
        /// Specifies the width of an "iframe"
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; }}

        /// <summary>
        /// Specifies the address of the document to embed in the "iframe"
        /// </summary>
        public SourceAttribute Src { get { return _sourceAttribute; }}

        /// <summary>
        /// Specifies the name of an "iframe"
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; }}

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
        public SourceDocAttribute SrcDoc { get { return _sourceDocAttribute; }}


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
