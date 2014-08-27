using XHTMLClassLibrary.AttributeDataTypes;
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
        private readonly SimpleSingleTypeAttribute<Color> _aLinkAttribute = new SimpleSingleTypeAttribute<Color>();

        [AttributeTypeAttributeMember(Name = "background", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<URI> _backGroundAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "bgcolor", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Color> _backgroundColorAttribute = new SimpleSingleTypeAttribute<Color>();

        [AttributeTypeAttributeMember(Name = "link", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Color> _linkAttribute = new SimpleSingleTypeAttribute<Color>();

        [AttributeTypeAttributeMember(Name = "text", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Color> _textAttribute = new SimpleSingleTypeAttribute<Color>();

        [AttributeTypeAttributeMember(Name = "vlink", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Color> _vLinkAttribute = new SimpleSingleTypeAttribute<Color>();



        /// <summary>
        /// Specifies the color of an active link in a document
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess ALink { get { return _aLinkAttribute; }}

        /// <summary>
        /// Specifies a background image for a document
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Background { get { return _backGroundAttribute; }}


        /// <summary>
        /// Specifies the background color of a document
        /// Not supported in HTML5. 
        /// </summary>
        public IAttributeDataAccess BackgroundColor { get { return _backgroundColorAttribute; }}

        /// <summary>
        /// Specifies the color of unvisited links in a document
        /// Not supported in HTML5. 
        /// </summary>
        public IAttributeDataAccess Link { get { return _linkAttribute; }}


        /// <summary>
        /// Specifies the color of visited links in a document
        /// Not supported in HTML5. 
        /// </summary>
        public IAttributeDataAccess VLink { get { return _vLinkAttribute; }}

        /// <summary>
        /// Specifies the color of the text in a document
        /// Not supported in HTML5. 
        /// </summary>
        public IAttributeDataAccess Text { get { return _textAttribute; }}

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
