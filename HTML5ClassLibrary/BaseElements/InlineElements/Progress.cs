using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "progress" tag represents the progress of a task. 
    /// </summary>
    [HTMLItemAttribute(ElementName = "progress", SupportedStandards = HTMLElementType.HTML5)]
    public class Progress: HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MaxAttribute _maxAttribute = new MaxAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly MeterValueAttribute _valueAttribute = new MeterValueAttribute();


        /// <summary>
        /// Specifies how much work the task requires in total
        /// </summary>
        public MaxAttribute Max { get { return _maxAttribute; }}

        /// <summary>
        /// Specifies how much of the task has been completed
        /// </summary>
        public MeterValueAttribute Value { get { return _valueAttribute; }}

        public override bool IsValid()
        {
            return true;
        }

        public override List<IHTMLItem> SubElements()
        {
            return null;
        }
    }
}
