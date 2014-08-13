using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Exceptions;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "meter" tag defines a scalar measurement within a known range, or a fractional value. This is also known as a gauge.
    /// Examples: Disk usage, the relevance of a query result, etc.
    /// Note: The "meter" tag should not be used to indicate progress (as in a progress bar). For progress bars, use the "progress" tag.
    ///</summary>
    public class Meter : BaseInlineItem
    {
        public const string ElementName = "meter";

        public Meter()
        {
            Attributes.Add(_formIdAttribute);
            Attributes.Add(_highAttribute);
            Attributes.Add(_lowAttribute);
            Attributes.Add(_maxAttribute);
            Attributes.Add(_minAttribute);
            Attributes.Add(_openAttribute);
            Attributes.Add(_meterValueAttribute);
        }

        private string _innerText;

        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly HighAttribute _highAttribute = new HighAttribute();
        private readonly LowAttribute _lowAttribute = new LowAttribute();
        private readonly MaxAttribute _maxAttribute = new MaxAttribute();
        private readonly MinAttribute _minAttribute = new MinAttribute();
        private readonly OptimumAttribute _openAttribute = new OptimumAttribute();
        private readonly MeterValueAttribute _meterValueAttribute = new MeterValueAttribute(); 

        /// <summary>
        /// Get/set content of the element to measure
        /// </summary>
        public string Content
        {
            get { return _innerText; }
            set { _innerText = value; }
        }

        /// <summary>
        /// Specifies one or more forms the "meter" element belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies the range that is considered to be a high value
        /// </summary>
        public HighAttribute High { get { return _highAttribute; }}

        /// <summary>
        /// Specifies the range that is considered to be a low value
        /// </summary>
        public LowAttribute Low { get { return _lowAttribute; }}

        /// <summary>
        /// Specifies the maximum value of the range
        /// </summary>
        public MaxAttribute Max { get { return _maxAttribute; }}

        /// <summary>
        /// Specifies the minimum value of the range
        /// </summary>
        public MinAttribute Min { get { return _minAttribute; }}

        /// <summary>
        /// Specifies what value is the optimal value for the gauge
        /// </summary>
        public OptimumAttribute Optimum { get { return _openAttribute; }}

        /// <summary>
        /// Required. Specifies the current value of the gauge
        /// </summary>
        public MeterValueAttribute Value { get { return _meterValueAttribute; }}


        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception("xNode is not empty line element");
            }
            ReadAttributes(xElement);
            if (!string.IsNullOrEmpty(xElement.Value))
            {
                _innerText = xElement.Value;
            }
        }

        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);
            AddAttributes(xElement);
            xElement.Value = _innerText;
            return xElement;
        }

        public override bool IsValid()
        {
            return _meterValueAttribute.HasValue();
        }

        public override void Add(IHTML5Item item)
        {
            throw new HTML5ViolationException(item, "");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new HTML5ViolationException(item,"");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }
    }
}
