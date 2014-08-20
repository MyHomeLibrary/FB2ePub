using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;

namespace XHTMLClassLibrary.BaseElements.FormMenuOptions
{
    /// <summary>
    /// The option element represents a choice offered by select form controls.
    /// </summary>
    [HTMLItemAttribute(ElementName = "option", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Option : HTMLItem, IOptionItem
    {
        [AttributeTypeAttributeMember(Name = "selected", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute  _selectedAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValueAttribute _valueAttribute = new ValueAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _disabledAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(Name = "label", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueAttribute _labelAttribute = new TextValueAttribute();


        /// <summary>
        /// When set, this attribute specifies that an option is pre-selected. 
        /// Possible value is "selected".
        /// </summary>
        public FlagAttribute Selected { get { return _selectedAttribute; } }

        /// <summary>
        /// Value associated with a selection option.
        /// </summary>
        public ValueAttribute Value { get { return _valueAttribute; } }

        /// <summary>
        /// Disables an option in a list of selectable options. 
        /// Possible value is "disabled".
        /// </summary>
        public FlagAttribute Disabled { get { return _disabledAttribute; } }

        /// <summary>
        /// Shorter label.
        /// </summary>
        public TextValueAttribute Label { get { return _labelAttribute; } }


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
