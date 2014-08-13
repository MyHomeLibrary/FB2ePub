using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;
using HTML5ClassLibrary.BaseElements.ListElements;

namespace HTML5ClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The ol element is used to create ordered lists. 
    /// An ordered list is a grouping of items whose sequence in the list is important. 
    /// For example, the sequence of steps in a recipe is important if the result is to be the intended one.
    /// </summary>
    public class OrderedList : BaseBlockElement
    {
        public const string ElementName = "ol";

        public OrderedList()
        {
            Attributes.Add(_reversedAttribute);
            Attributes.Add(_orderedListStartAttribute);
            Attributes.Add(_orderedListTypeAttribute);
        }

        private readonly ReversedAttribute _reversedAttribute = new ReversedAttribute();
        private readonly OrderedListStartAttribute _orderedListStartAttribute = new OrderedListStartAttribute();
        private readonly OrderedListTypeAttribute _orderedListTypeAttribute = new OrderedListTypeAttribute();



        /// <summary>
        /// Specifies the start value of an ordered list
        /// </summary>
        public OrderedListStartAttribute Start { get { return _orderedListStartAttribute; }}

        /// <summary>
        /// Specifies the kind of marker to use in the list
        /// </summary>
        public OrderedListTypeAttribute Type { get { return _orderedListTypeAttribute; }}

        /// <summary>
        /// Specifies that the list order should be descending (9,8,7...)
        /// </summary>
        public ReversedAttribute Reversed { get { return _reversedAttribute; }}

        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            XElement xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }

            ReadAttributes(xElement);

            Content.Clear();
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
                    Content.Add(item);
                }
            }

        }

        protected override bool IsValidSubType(IHTML5Item item)
        {
            if (item is ListItem)
            {
                return item.IsValid();
            }
            return false;
        }

        public override XNode Generate()
        {
            XElement xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            foreach (var item in Content)
            {
                xElement.Add(item.Generate());
            }
            return xElement;
        }

        public override bool IsValid()
        {
            return (Content.Count > 0);
        }
    }
}
