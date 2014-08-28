using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;
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
        #region Attribute_Values_Enums

        /// <summary>
        /// "type" attribute possible values
        /// </summary>
        public enum OrderedListTypeAttributeOptions
        {
            [Description("1")]
            Diggit,

            [Description("A")]
            CapitalA,

            [Description("a")]
            SmallA,

            [Description("I")]
            CapitalI,

            [Description("i")]
            SmallI,          
        }

        #endregion

        [AttributeTypeAttributeMember(Name = "compact", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _compactAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "reversed", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _reversedAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "high", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Number> _orderedListStartAttribute = new SimpleSingleTypeAttribute<Number>();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValuesSelectionTypeAttribute<Text> _orderedListTypeAttribute = new ValuesSelectionTypeAttribute<Text>(typeof(OrderedListTypeAttributeOptions));





        /// <summary>
        ///  Specifies that the list should render smaller than normal
        /// Not supported in HTML5.
        /// </summary>
        public IAttributeDataAccess Compact { get { return _compactAttribute; }}

        /// <summary>
        /// Specifies the start value of an ordered list
        /// </summary>
        public IAttributeDataAccess Start { get { return _orderedListStartAttribute; } }

        /// <summary>
        /// Specifies the kind of marker to use in the list
        /// </summary>
        public IAttributeDataAccess Type { get { return _orderedListTypeAttribute; } }

        /// <summary>
        /// Specifies that the list order should be descending (9,8,7...)
        /// </summary>
        public IAttributeDataAccess Reversed { get { return _reversedAttribute; } }


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
