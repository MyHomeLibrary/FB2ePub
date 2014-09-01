using System.Collections.Generic;
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
        private readonly SimpleSingleTypeAttribute<Text> _maxAttribute = new SimpleSingleTypeAttribute<Text>("max");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly SimpleSingleTypeAttribute<FloatingNumber> _valueAttribute = new SimpleSingleTypeAttribute<FloatingNumber>("value");


        /// <summary>
        /// Specifies how much work the task requires in total
        /// </summary>
        public IAttributeDataAccess Max { get { return _maxAttribute; }}

        /// <summary>
        /// Specifies how much of the task has been completed
        /// </summary>
        public IAttributeDataAccess Value { get { return _valueAttribute; } }

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
