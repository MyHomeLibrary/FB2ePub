using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace XHTMLClassLibrary.BaseElements.Structure_Header
{
    /// <summary>
    /// The body element contains the contents of a Web page.
    /// </summary>
    [HTMLItemAttribute(ElementName = "body", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Body : HTMLItem
    {
        [AttributeTypeAttributeMember(Name = "alink", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ColorTypeAttribute _aLinkAttribute = new ColorTypeAttribute();

        [AttributeTypeAttributeMember(Name = "background", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _backGroundAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "bgcolor", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ColorTypeAttribute _backgroundColorAttribute = new ColorTypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LinkAttribute _linkAttribute = new LinkAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextColorAttribute _textAttribute = new TextColorAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly VLinkAttribute _vLinkAttribute = new VLinkAttribute();



        /// <summary>
        /// Specifies the color of an active link in a document
        /// Not supported in HTML5.
        /// </summary>
        public ColorTypeAttribute ALink { get { return _aLinkAttribute; }}

        /// <summary>
        /// Specifies a background image for a document
        /// Not supported in HTML5.
        /// </summary>
        public URITypeAttribute Background { get { return _backGroundAttribute; }}


        /// <summary>
        /// Specifies the background color of a document
        /// Not supported in HTML5. 
        /// </summary>
        public ColorTypeAttribute BackgroundColor { get { return _backgroundColorAttribute; }}

        /// <summary>
        /// Specifies the color of unvisited links in a document
        /// Not supported in HTML5. 
        /// </summary>
        public LinkAttribute Link { get { return _linkAttribute; }}


        /// <summary>
        /// Specifies the color of visited links in a document
        /// Not supported in HTML5. 
        /// </summary>
        public VLinkAttribute VLink { get { return _vLinkAttribute; }}

        /// <summary>
        /// Specifies the color of the text in a document
        /// Not supported in HTML5. 
        /// </summary>
        public TextColorAttribute Text { get { return _textAttribute; }}

        public override bool IsValid()
        {
            // body can't be empty
            return (Subitems.Count > 0);
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
