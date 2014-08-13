using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.Events;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;
using HTML5ClassLibrary.BaseElements.BlockElements;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The script element places a client-side script, such as JavaScript, within a document. 
    /// This element may appear any number of times in the head or body of a Web page. 
    /// The script element may contain a script (called an embedded script) or 
    /// point via the src attribute to a file containing a script (an external script).
    /// </summary>
    public class Script : BaseInlineItem, IBlockElement
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

        internal const string ElementName = "script";



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


        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }

            ReadAttributes(xElement);

            _scriptText.Load(xNode);
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            xElement.Add(_scriptText.Generate());

            return xElement;

        }

        public override bool IsValid()
        {
            return (_contentTypeAttribute.HasValue());
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }
    }
}
