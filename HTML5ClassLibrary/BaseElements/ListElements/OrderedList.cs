using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace XHTMLClassLibrary.BaseElements.ListElements
{
    /// <summary>
    /// The ol element is used to create ordered lists. 
    /// An ordered list is a grouping of items whose sequence in the list is important. 
    /// For example, the sequence of steps in a recipe is important if the result is to be the intended one.
    /// </summary>
    [HTMLItemAttribute(ElementName = "ol", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]    
    public class OrderedList : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(Name = "compact", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _compactAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(Name = "reversed", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _reversedAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OrderedListStartAttribute _orderedListStartAttribute = new OrderedListStartAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OrderedListTypeAttribute _orderedListTypeAttribute = new OrderedListTypeAttribute();





        /// <summary>
        ///  Specifies that the list should render smaller than normal
        /// Not supported in HTML5.
        /// </summary>
        public FlagAttribute Compact { get { return _compactAttribute; }}

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
        public FlagAttribute Reversed { get { return _reversedAttribute; }}


        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is ListItem)
            {
                return item.IsValid();
            }
            return false;
        }


        public override bool IsValid()
        {
            return (Subitems.Count > 0);
        }
    }
}
