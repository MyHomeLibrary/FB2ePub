using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "progress" tag represents the progress of a task. 
    /// </summary>
    [HTMLItemAttribute(ElementName = "progress", SupportedStandards = HTMLElementType.HTML5)]
    public class Progress: HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "max", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _maxAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "value", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FloatingNumberTypeAttribute _valueAttribute = new FloatingNumberTypeAttribute();


        /// <summary>
        /// Specifies how much work the task requires in total
        /// </summary>
        public TextValueTypeAttribute Max { get { return _maxAttribute; }}

        /// <summary>
        /// Specifies how much of the task has been completed
        /// </summary>
        public FloatingNumberTypeAttribute Value { get { return _valueAttribute; }}

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
