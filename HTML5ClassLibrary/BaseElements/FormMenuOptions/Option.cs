using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.FlaggedAttributes;

namespace HTMLClassLibrary.BaseElements.FormMenuOptions
{
    /// <summary>
    /// The option element represents a choice offered by select form controls.
    /// </summary>
    [HTMLItemAttribute(ElementName = "option", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]
    public class Option : HTMLItem, IOptionItem
    {
        public Option()
        {
            RegisterAttribute(_selectedAttribute);
            RegisterAttribute(_valueAttribute);
            RegisterAttribute(_disabledAttribute);
            RegisterAttribute(_labelAttribute);
        }

        private readonly SimpleHTML5Text _optionText = new SimpleHTML5Text();

        private readonly SelectedAttribute  _selectedAttribute = new SelectedAttribute();
        private readonly ValueAttribute _valueAttribute = new ValueAttribute();
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();
        private readonly LabelAttribute _labelAttribute = new LabelAttribute();

        /// <summary>
        /// The script text itself
        /// </summary>
        public SimpleHTML5Text OptionText { get { return _optionText; } }


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
