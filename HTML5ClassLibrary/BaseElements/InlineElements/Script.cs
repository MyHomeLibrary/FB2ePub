using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The script element places a client-side script, such as JavaScript, within a document. 
    /// This element may appear any number of times in the head or body of a Web page. 
    /// The script element may contain a script (called an embedded script) or 
    /// point via the src attribute to a file containing a script (an external script).
    /// </summary>
    [HTMLItemAttribute(ElementName = "script", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Script : HTMLItem, IInlineItem, IBlockElement
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly AsyncAttribute _asyncAttribute = new AsyncAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly CharsetAttribute _charsetAttribute = new CharsetAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly DeferAttribute _deferAttribute = new DeferAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SourceAttribute _srcAttribute = new SourceAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ContentTypeAttribute _contentTypeAttribute = new ContentTypeAttribute();

        // xml:space 	preserve 	Not supported in HTML5. Specifies whether whitespace in code should be preserved




        /// <summary>
        /// Location of an external script.
        /// </summary>
        public SourceAttribute Src { get { return _srcAttribute; } }

        /// <summary>
        /// Specifies that the script is executed asynchronously (only for external scripts)
        /// </summary>
        public AsyncAttribute Async { get { return _asyncAttribute; }}

        /// <summary>
        /// This attribute specifies the scripting language of the element's contents. 
        /// The scripting language is specified as a content type. For example: text/javascript. 
        /// This attribute is required.
        /// </summary>
        public ContentTypeAttribute Type { get { return _contentTypeAttribute; } }

        /// <summary>
        /// Character encoding of the resource designated by src.
        /// </summary>
        public CharsetAttribute Charset { get { return _charsetAttribute; } }

        /// <summary>
        /// When set, this attribute provides a hint to the Web browser that the script is not going to generate any document content (no document.write in javascript for example), 
        /// permitting the Web browser to continue parsing and rendering the rest of the page. 
        /// Possible value is defer.
        /// </summary>
        public DeferAttribute Defer { get { return _deferAttribute; } }


        public override bool IsValid()
        {
            return (_contentTypeAttribute.HasValue());
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
