using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTMLClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTMLClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTMLClassLibrary.Attributes.Events;
using HTMLClassLibrary.Attributes.FlaggedAttributes;
using HTMLClassLibrary.BaseElements.BlockElements;

namespace HTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The script element places a client-side script, such as JavaScript, within a document. 
    /// This element may appear any number of times in the head or body of a Web page. 
    /// The script element may contain a script (called an embedded script) or 
    /// point via the src attribute to a file containing a script (an external script).
    /// </summary>
    [HTMLItemAttribute(ElementName = "script", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Script : HTMLItem, IInlineItem, IBlockElement
    {
        private readonly SimpleHTML5Text _scriptText = new SimpleHTML5Text();

        public Script()
        {
            RegisterAttribute(_srcAttribute);
            RegisterAttribute(_contentTypeAttribute);
            RegisterAttribute(_charsetAttribute);
            RegisterAttribute(_deferAttribute);
            RegisterAttribute(_asyncAttribute);
        }
   
        private readonly SourceAttribute _srcAttribute = new SourceAttribute();
        private readonly ContentTypeAttribute _contentTypeAttribute = new ContentTypeAttribute();
        private readonly CharsetAttribute _charsetAttribute = new CharsetAttribute();
        private readonly DeferAttribute _deferAttribute = new DeferAttribute();
        private readonly AsyncAttribute _asyncAttribute = new AsyncAttribute();

        /// <summary>
        /// The script text itself
        /// </summary>
        public SimpleHTML5Text ScriptText { get { return _scriptText; } }

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
