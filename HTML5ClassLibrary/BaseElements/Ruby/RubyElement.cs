using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MediaEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.WindowEventAttributes;
using HTML5ClassLibrary.BaseElements.InlineElements;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.Ruby
{
    /// <summary>
    /// Ruby is mechanism for adding annotations to characters of East Asian languages such as Chinese and Japanese. 
    /// These annotations typically appear in smaller typeface above or to the side of regular text, 
    /// and are meant to help with pronunciation of obscure characters or as a language learning aid.
    /// </summary>
    public class RubyElement : IInlineItem
    {

        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTML5Item> _content = new List<IHTML5Item>();

        private readonly HTMLGlobalAttributes _globalAttributes = new HTMLGlobalAttributes();
        private readonly FormEvents _formEvents = new FormEvents();
        private readonly KeyboardEvents _keyboardEvents = new KeyboardEvents();
        private readonly MediaEvents _mediaEvents = new MediaEvents();
        private readonly MouseEvents _mouseEvents = new MouseEvents();
        private readonly WindowEventAttributes _windowEventAttributes = new WindowEventAttributes();


        internal const string ElementName = "ruby";

        public static XNamespace XhtmlNameSpace = @"http://www.w3.org/1999/xhtml";


        public HTMLGlobalAttributes GlobalAttributes { get { return _globalAttributes; }}

        public FormEvents FormEvents { get { return _formEvents; } }

        public KeyboardEvents KeyboardEvents { get { return _keyboardEvents; } }

        public MediaEvents MediaEvents { get { return _mediaEvents; } }

        public MouseEvents MouseEvents { get { return _mouseEvents; } }

        public WindowEventAttributes WindowEvents { get { return _windowEventAttributes; } }


        #region Implementation of IEPubTextItem

        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
        public void Load(XNode xNode)
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

            _content.Clear();
            IEnumerable<XNode> descendants = xElement.Nodes();
            foreach (var node in descendants)
            {
                IHTML5Item item = ElementFactory.CreateElement(node);
                if ((item != null) && IsValidSubType(item))
                {
                    try
                    {
                        item.Load(node);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    _content.Add(item);
                }
            }

            _globalAttributes.ReadAttributes(xElement);
            _formEvents.ReadAttributes(xElement);
            _keyboardEvents.ReadAttributes(xElement);
            _mediaEvents.ReadAttributes(xElement);
            _mouseEvents.ReadAttributes(xElement);
            _windowEventAttributes.ReadAttributes(xElement);
        }

        private bool IsValidSubType(IHTML5Item item)
        {
            // TODO: full check for ruby sequence
            if (item is IRubyItem)
            {
                return item.IsValid();
            }
            return false;
        }

        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>generated XNode</returns>
        public XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            if (_content.Count > 0)
            {
                foreach (var item in _content)
                {
                    xElement.Add(item.Generate());
                }
            }

            _globalAttributes.AddAttributes(xElement);
            _formEvents.AddAttributes(xElement);
            _keyboardEvents.AddAttributes(xElement);
            _mediaEvents.AddAttributes(xElement);
            _mouseEvents.AddAttributes(xElement);
            _windowEventAttributes.AddAttributes(xElement);
            return xElement;
        }

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>true if valid</returns>
        public bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public void Add(IHTML5Item item)
        {
            if ((item != null) && IsValidSubType(item))
            {
                _content.Add(item);
                item.Parent = this;
            }
            else
            {
                throw new HTML5ViolationException(item,"");
            }
        }

        public void Remove(IHTML5Item item)
        {
            if(_content.Remove(item))
            {
                item.Parent = null;
            }
        }

        public List<IHTML5Item> SubElements()
        {
            return _content;
        }

        /// <summary>
        /// Get/Set item parent in the XHTML "tree"
        /// </summary>
        public IHTML5Item Parent { get; set; }

        #endregion

    }
}
