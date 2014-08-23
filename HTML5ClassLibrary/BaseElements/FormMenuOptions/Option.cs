using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.FormMenuOptions
{
    /// <summary>
    /// The option element represents a choice offered by select form controls.
    /// </summary>
    [HTMLItemAttribute(ElementName = "option", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Option : HTMLItem, IOptionItem
    {
        [AttributeTypeAttributeMember(Name = "selected", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute  _selectedAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "value", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _valueAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _disabledAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "label", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _labelAttribute = new TextValueTypeAttribute();


        /// <summary>
        /// When set, this attribute specifies that an option is pre-selected. 
        /// Possible value is "selected".
        /// </summary>
        public FlagTypeAttribute Selected { get { return _selectedAttribute; } }

        /// <summary>
        /// Value associated with a selection option.
        /// </summary>
        public TextValueTypeAttribute Value { get { return _valueAttribute; } }

        /// <summary>
        /// Disables an option in a list of selectable options. 
        /// Possible value is "disabled".
        /// </summary>
        public FlagTypeAttribute Disabled { get { return _disabledAttribute; } }

        /// <summary>
        /// Shorter label.
        /// </summary>
        public TextValueTypeAttribute Label { get { return _labelAttribute; } }


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
