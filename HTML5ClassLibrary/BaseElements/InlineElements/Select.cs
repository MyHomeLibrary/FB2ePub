using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements.FormMenuOptions;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The select element is used to create an option selector form control which most Web browsers render as a listbox control. 
    /// The list of values for this control is created using option elements. 
    /// These values can be grouped together using the optgroup element.
    /// </summary>
    [HTMLItemAttribute(ElementName = "select", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet | HTMLElementType.XHTML11)]    
    public class Select : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "autofocus", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _autoFocusAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _disabledAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();

        [AttributeTypeAttributeMember(Name = "multiple", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _multipleAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameAttribute _nameAttribute = new NameAttribute();

        [AttributeTypeAttributeMember(Name = "required", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _requiredAttribute = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly SizeAttribute _sizeAttribute = new SizeAttribute();
        


        /// <summary>
        /// If set, this attribute allows multiple selections. 
        /// Possible value is multiple.
        /// </summary>
        public FlagAttribute Multiple { get { return _multipleAttribute; } }

        /// <summary>
        /// Form control name.
        /// </summary>
        public NameAttribute Name { get { return _nameAttribute; } }


        /// <summary>
        /// Number of rows in the list that should be visible at the same time.
        /// </summary>
        public SizeAttribute Size { get { return _sizeAttribute; } }

        /// <summary>
        /// Disables the control for user input. 
        /// Possible value is disabled.
        /// </summary>
        public FlagAttribute Disabled { get { return _disabledAttribute; } }

        /// <summary>
        /// Specifies that the drop-down list should automatically get focus when the page loads
        /// </summary>
        public FlagAttribute Autofocus { get { return _autoFocusAttribute; }}

        /// <summary>
        /// Defines one or more forms the select field belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}

        /// <summary>
        /// Specifies that the user is required to select a value before submitting the form
        /// </summary>
        public FlagAttribute Required { get { return _requiredAttribute; }}



        public override bool IsValid()
        {
            // at least one of sub elements have to appear
            return (Subitems.Count > 0);
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IOptionItem)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
