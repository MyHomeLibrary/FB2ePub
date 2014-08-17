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
        public OrderedList()
        {
            RegisterAttribute(_reversedAttribute);
            RegisterAttribute(_orderedListStartAttribute);
            RegisterAttribute(_orderedListTypeAttribute);
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
