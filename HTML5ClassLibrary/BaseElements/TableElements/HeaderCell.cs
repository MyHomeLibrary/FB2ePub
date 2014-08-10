using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.BaseElements.BlockElements;
using HTML5ClassLibrary.BaseElements.InlineElements;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.TableElements
{
    /// <summary>
    /// The th element defines a table header cell.
    /// </summary>
    public class HeaderCell : BaseTableElement
    {
        internal const string ElementName = "th";

        private readonly List<IHTML5Item> _content = new List<IHTML5Item>();

        // Basic attributes
        private readonly AbbreviatedAttribute _abbrAttribute = new AbbreviatedAttribute();
        private readonly ColSpanAttribute _colSpanAttribute = new ColSpanAttribute();
        private readonly HeadersAttribute _headersAttribute = new HeadersAttribute();
        private readonly RowSpanAttribute _rowSpanAttribue = new RowSpanAttribute();
        private readonly ScopeAttribute _scopeAttribute = new ScopeAttribute();
        private readonly SortedAttribute _sortedAttribute = new SortedAttribute();



        /// <summary>
        /// Abbreviated form of the cell's content.
        /// </summary>
        public AbbreviatedAttribute Abbr { get { return _abbrAttribute; } }


        /// <summary>
        /// This attribute specifies the number of columns spanned by the current cell.
        /// </summary>
        public ColSpanAttribute ColSpan { get { return _colSpanAttribute; } }


        /// <summary>
        /// Specifies one or more header cells a cell is related to
        /// </summary>
        public HeadersAttribute Headers { get { return _headersAttribute; }}

        /// <summary>
        /// This attribute specifies the number of rows spanned by the current cell.
        /// </summary>
        public RowSpanAttribute RowSpan { get { return _rowSpanAttribue; } }


        /// <summary>
        /// Defines the sort direction of a column
        /// </summary>
        public SortedAttribute Sorted { get { return _sortedAttribute; }}


        /// <summary>
        ///     This attribute specifies the set of data cells for which the current header cell provides header information. 
        /// When specified, this attribute must have one of the following values:
        /// 
        /// * row: The current cell provides header information for the rest of the row that contains it.
        /// * col: The current cell provides header information for the rest of the column that contains it.
        /// * rowgroup: The header cell provides header information for the rest of the row group that contains it.
        /// * colgroup: The header cell provides header information for the rest of the column group that contains it.
        /// </summary>
        public ScopeAttribute Scope { get { return _scopeAttribute; } }


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
            if (item is IInlineItem)
            {
                return item.IsValid();
            }
            if (item is IBlockElement)
            {
                return item.IsValid();
            }
            if (item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }


        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            foreach (var item in _content)
            {
                xElement.Add(item.Generate());
            }
            return xElement;
        }

        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            if ((item != null) && IsValidSubType(item))
            {
                _content.Add(item);
                item.Parent = this;
            }
            else
            {
                throw new HTML5ViolationException();
            }
        }

        public override void Remove(IHTML5Item item)
        {
            if(_content.Remove(item))
            {
                item.Parent = null;
            }
        }


        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _abbrAttribute.AddAttribute(xElement);
            _colSpanAttribute.AddAttribute(xElement);
            _headersAttribute.AddAttribute(xElement);
            _rowSpanAttribue.AddAttribute(xElement);
            _scopeAttribute.AddAttribute(xElement);
            _sortedAttribute.AddAttribute(xElement);
        }

        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _abbrAttribute.ReadAttribute(xElement);
            _colSpanAttribute.ReadAttribute(xElement);
            _headersAttribute.ReadAttribute(xElement);
            _rowSpanAttribue.ReadAttribute(xElement);
            _scopeAttribute.ReadAttribute(xElement);
            _sortedAttribute.ReadAttribute(xElement);
        }

        public override List<IHTML5Item> SubElements()
        {
            return _content;
        }
    }
}
