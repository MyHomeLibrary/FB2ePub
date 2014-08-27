using System.Collections.Generic;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "meter" tag defines a scalar measurement within a known range, or a fractional value. This is also known as a gauge.
    /// Examples: Disk usage, the relevance of a query result, etc.
    /// Note: The "meter" tag should not be used to indicate progress (as in a progress bar). For progress bars, use the "progress" tag.
    ///</summary>
    [HTMLItemAttribute(ElementName = "meter", SupportedStandards = HTMLElementType.HTML5 )]
    public class Meter : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "form", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<URI> _formIdAttribute = new SimpleSingleTypeAttribute<URI>();

        [AttributeTypeAttributeMember(Name = "high", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<FloatingNumber> _highAttribute = new SimpleSingleTypeAttribute<FloatingNumber>();

        [AttributeTypeAttributeMember(Name = "low", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<FloatingNumber> _lowAttribute = new SimpleSingleTypeAttribute<FloatingNumber>();

        [AttributeTypeAttributeMember(Name = "max", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _maxAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "min", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<Text> _minAttribute = new SimpleSingleTypeAttribute<Text>();

        [AttributeTypeAttributeMember(Name = "optimum", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<FloatingNumber> _openAttribute = new SimpleSingleTypeAttribute<FloatingNumber>();

        [AttributeTypeAttributeMember(Name = "value", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<FloatingNumber> _meterValueAttribute = new SimpleSingleTypeAttribute<FloatingNumber>(); 


        /// <summary>
        /// Specifies one or more forms the "meter" element belongs to
        /// </summary>
        public IAttributeDataAccess Form { get { return _formIdAttribute; } }

        /// <summary>
        /// Specifies the range that is considered to be a high value
        /// </summary>
        public IAttributeDataAccess High { get { return _highAttribute; } }

        /// <summary>
        /// Specifies the range that is considered to be a low value
        /// </summary>
        public IAttributeDataAccess Low { get { return _lowAttribute; } }

        /// <summary>
        /// Specifies the maximum value of the range
        /// </summary>
        public IAttributeDataAccess Max { get { return _maxAttribute; } }

        /// <summary>
        /// Specifies the minimum value of the range
        /// </summary>
        public IAttributeDataAccess Min { get { return _minAttribute; } }

        /// <summary>
        /// Specifies what value is the optimal value for the gauge
        /// </summary>
        public IAttributeDataAccess Optimum { get { return _openAttribute; } }

        /// <summary>
        /// Required. Specifies the current value of the gauge
        /// </summary>
        public IAttributeDataAccess Value { get { return _meterValueAttribute; } }


        public override bool IsValid()
        {
            return _meterValueAttribute.HasValue();
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
