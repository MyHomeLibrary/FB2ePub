using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes.RubyAttributes;
using HTML5ClassLibrary.BaseElements.InlineElements;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.Ruby
{
    /// <summary>
    /// The rt element contains ruby text annotations.
    /// </summary>
    public class RtElement : BaseRubyItem
    {
        /// <summary>
        /// Internal content of the element
        /// </summary>
        private readonly List<IHTML5Item> _content = new List<IHTML5Item>();

        internal const string ElementName = "rt";

        #region Overrides of BaseRubyItem


        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
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

        }

        private bool IsValidSubType(IHTML5Item item)
        {
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            if (item is IInlineItem)
            {
                if (item is RubyElement)
                {
                    return false;
                }
                return item.IsValid();
            }
            return false;
        }

        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>
        /// generated XNode
        /// </returns>
        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAtributes(xElement);

            if (_content.Count > 0)
            {
                foreach (var item in _content)
                {
                    xElement.Add(item.Generate());
                }
            }
            return xElement;
        }

        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds subitem to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">subitem to add</param>
        public override void Add(IHTML5Item item)
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

        public override void Remove(IHTML5Item item)
        {
            if(_content.Remove(item))
            {
                item.Parent = null;
            }
        }

        public override List<IHTML5Item> SubElements()
        {
            return _content;
        }

        #endregion
    }
}
