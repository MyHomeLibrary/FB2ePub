using System.Collections.Generic;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.FormMenuOptions
{
    /// <summary>
    /// The option element represents a choice offered by select form controls.
    /// </summary>
    [HTMLItemAttribute(ElementName = "option", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Option : HTMLItem, IOptionItem
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute  _selectedAttribute = new FlagTypeAttribute("selected");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _valueAttribute = new SimpleSingleTypeAttribute<Text>("value");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _disabledAttribute = new FlagTypeAttribute("disabled");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SimpleSingleTypeAttribute<Text> _labelAttribute = new SimpleSingleTypeAttribute<Text>("label");


        /// <summary>
        /// When set, this attribute specifies that an option is pre-selected. 
        /// Possible value is "selected".
        /// </summary>
        public IAttributeDataAccess Selected { get { return _selectedAttribute; } }

        /// <summary>
        /// Value associated with a selection option.
        /// </summary>
        public IAttributeDataAccess Value { get { return _valueAttribute; } }

        /// <summary>
        /// Disables an option in a list of selectable options. 
        /// Possible value is "disabled".
        /// </summary>
        public IAttributeDataAccess Disabled { get { return _disabledAttribute; } }

        /// <summary>
        /// Shorter label.
        /// </summary>
        public IAttributeDataAccess Label { get { return _labelAttribute; } }


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
