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
    /// The td element defines a data cell in a table (i.e. cells that are not header cells).
    /// </summary>
    public class TableData : BaseTableElement
    {
        public const string ElementName = "td";

        private readonly List<IHTML5Item> _content = new List<IHTML5Item>();

        // Basic attributes
        private readonly AbbreviatedAttribute _abbrAttribute = new AbbreviatedAttribute();
        private readonly AlignAttribute _alignAttribute = new AlignAttribute();
        private readonly ColSpanAttribute _colSpanAttribute = new ColSpanAttribute();
        private readonly RowSpanAttribute _rowSpanAttribue = new RowSpanAttribute();
        private readonly VAlignAttribute _valignAttribute = new VAlignAttribute();

        // Advanced attributes 
        private readonly AxisAttribute _axisAttribute = new AxisAttribute();
        private readonly CharAttribute _charAttribute = new CharAttribute();
        private readonly CharOffAttribute _charOffAtribute = new CharOffAttribute();

        /// <summary>
        /// Horizontal alignment in cells. Possible values are:
        /// 
        /// * left: Left-justify text. This is the default value for table data cells.
        /// * center: Center-justify text. This is the default value for table header cells.
        /// * right: Right-justify text.
        /// * justify: Left- and right-justify text.
        /// * char: Align text around a specific character.
        /// </summary>
        public AlignAttribute Align { get { return _alignAttribute; } }


        /// <summary>
        /// Vertical alignment in cells. Possible values are:
        /// 
        /// * top: Cell data is flush with the top of the cell.
        /// * middle: Cell data is centered vertically within the cell. This is the default value.
        /// * bottom: Cell data is flush with the bottom of the cell.
        /// * baseline: All cells found in the same row as a cell whose valign attribute has this value will have their textual data positioned so that the first text line occurs on a baseline common to all cells in the row.
        /// </summary>
        public VAlignAttribute VerticalAlign { get { return _valignAttribute; } }


        /// <summary>
        /// his attribute specifies a single character within a text fragment to act as an axis for alignment. 
        /// The default value for this attribute is the decimal point character for the current language as set by the xml:lang attribute. 
        /// For example, the period (".") in English and the comma (",") in French.
        /// </summary>
        public CharAttribute AlignChar { get { return _charAttribute; } }


        /// <summary>
        /// When present, this attribute specifies the offset to the first occurrence of the alignment character on each line.
        /// </summary>
        public CharOffAttribute CharOff { get { return _charOffAtribute; } }

        /// <summary>
        /// Abbreviated form of the cell's content.
        /// </summary>
        public AbbreviatedAttribute Abbr { get { return _abbrAttribute; } }


        /// <summary>
        /// This attribute specifies the number of columns spanned by the current cell.
        /// </summary>
        public ColSpanAttribute ColSpan { get { return _colSpanAttribute; } }


        /// <summary>
        /// This attribute specifies the number of rows spanned by the current cell.
        /// </summary>
        public RowSpanAttribute RowSpan { get { return _rowSpanAttribue; } }


        /// <summary>
        /// This attribute may be used to place a cell into conceptual categories that can be considered to form axes in an n-dimensional space. 
        /// Some Web browsers or devices may present cells grouped by these categories.
        /// </summary>
        public AxisAttribute Axis { get { return _axisAttribute; } }



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

            // Base attributes
            _abbrAttribute.ReadAttribute(xElement);
            _alignAttribute.ReadAttribute(xElement);
            _colSpanAttribute.ReadAttribute(xElement);
            _rowSpanAttribue.ReadAttribute(xElement);
            _valignAttribute.ReadAttribute(xElement);

            // Advanced attributes
            _axisAttribute.ReadAttribute(xElement);
            _charAttribute.ReadAttribute(xElement);
            _charOffAtribute.ReadAttribute(xElement);

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

            // Base attributes
            _abbrAttribute.AddAttribute(xElement);
            _alignAttribute.AddAttribute(xElement);
            _colSpanAttribute.AddAttribute(xElement);
            _rowSpanAttribue.AddAttribute(xElement);
            _valignAttribute.AddAttribute(xElement);

            // Advanced attributes
            _axisAttribute.AddAttribute(xElement);
            _charAttribute.AddAttribute(xElement);
            _charOffAtribute.AddAttribute(xElement);

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

        public override List<IHTML5Item> SubElements()
        {
            return _content;
        }
    }
}
