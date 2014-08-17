using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
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
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ALinkAttribute _aLinkAttribute = new ALinkAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BackGroundAttribute _backGroundAttribute = new BackGroundAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BackgroundColorAttribute _backgroundColorAttribute = new BackgroundColorAttribute();

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
        public ALinkAttribute ALink { get { return _aLinkAttribute; }}

        /// <summary>
        /// Specifies a background image for a document
        /// Not supported in HTML5.
        /// </summary>
        public BackGroundAttribute Background { get { return _backGroundAttribute; }}


        /// <summary>
        /// Specifies the background color of a document
        /// Not supported in HTML5. 
        /// </summary>
        public BackgroundColorAttribute BackgroundColor { get { return _backgroundColorAttribute; }}

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
