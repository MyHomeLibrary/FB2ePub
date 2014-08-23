using System.Collections.Generic;
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
        private readonly URITypeAttribute _formIdAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "high", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FloatingNumberTypeAttribute _highAttribute = new FloatingNumberTypeAttribute();

        [AttributeTypeAttributeMember(Name = "low", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FloatingNumberTypeAttribute _lowAttribute = new FloatingNumberTypeAttribute();

        [AttributeTypeAttributeMember(Name = "max", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _maxAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "min", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _minAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "optimum", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FloatingNumberTypeAttribute _openAttribute = new FloatingNumberTypeAttribute();

        [AttributeTypeAttributeMember(Name = "value", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FloatingNumberTypeAttribute _meterValueAttribute = new FloatingNumberTypeAttribute(); 


        /// <summary>
        /// Specifies one or more forms the "meter" element belongs to
        /// </summary>
        public URITypeAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies the range that is considered to be a high value
        /// </summary>
        public FloatingNumberTypeAttribute High { get { return _highAttribute; }}

        /// <summary>
        /// Specifies the range that is considered to be a low value
        /// </summary>
        public FloatingNumberTypeAttribute Low { get { return _lowAttribute; }}

        /// <summary>
        /// Specifies the maximum value of the range
        /// </summary>
        public TextValueTypeAttribute Max { get { return _maxAttribute; }}

        /// <summary>
        /// Specifies the minimum value of the range
        /// </summary>
        public TextValueTypeAttribute Min { get { return _minAttribute; }}

        /// <summary>
        /// Specifies what value is the optimal value for the gauge
        /// </summary>
        public FloatingNumberTypeAttribute Optimum { get { return _openAttribute; }}

        /// <summary>
        /// Required. Specifies the current value of the gauge
        /// </summary>
        public FloatingNumberTypeAttribute Value { get { return _meterValueAttribute; }}


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
