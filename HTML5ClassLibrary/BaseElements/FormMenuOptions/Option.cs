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
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SelectedAttribute  _selectedAttribute = new SelectedAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValueAttribute _valueAttribute = new ValueAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly LabelAttribute _labelAttribute = new LabelAttribute();


        /// <summary>
        /// When set, this attribute specifies that an option is pre-selected. 
        /// Possible value is "selected".
        /// </summary>
        public SelectedAttribute Selected { get { return _selectedAttribute; } }

        /// <summary>
        /// Value associated with a selection option.
        /// </summary>
        public ValueAttribute Value { get { return _valueAttribute; } }

        /// <summary>
        /// Disables an option in a list of selectable options. 
        /// Possible value is "disabled".
        /// </summary>
        public DisabledAttribute Disabled { get { return _disabledAttribute; } }

        /// <summary>
        /// Shorter label.
        /// </summary>
        public LabelAttribute Label { get { return _labelAttribute; } }


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
