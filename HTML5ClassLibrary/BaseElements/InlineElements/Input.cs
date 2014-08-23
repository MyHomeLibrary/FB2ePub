using System.Collections.Generic;
using XHTMLClassLibrary.Attributes;

namespace XHTMLClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "input" tag specifies an input field where the user can enter data.
    /// "input" elements are used within a "form" element to declare input controls that allow users to input data.
    /// An input field can vary in many ways, depending on the type attribute.
    /// </summary>
    [HTMLItemAttribute(ElementName = "input", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]    
    public class Input : HTMLItem, IInlineItem
    {
        [AttributeTypeAttributeMember(Name = "accept", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ContentTypeAttribute _acceptAttribute = new ContentTypeAttribute();

         [AttributeTypeAttributeMember(Name = "align", SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly BiDirectionalAlignTypeAttribute _alignAttribute = new BiDirectionalAlignTypeAttribute();

        [AttributeTypeAttributeMember(Name = "alt", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _altAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "autocomplete", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnOffTypeAttribute _autocompleteAttribute = new OnOffTypeAttribute();

        [AttributeTypeAttributeMember(Name = "autofocus", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _autoFocusAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "checked", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _checkedAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _disabledAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "form", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _formIdAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "formaction", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly URITypeAttribute _formActionAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "enctype", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormEncodingTypeAttribute _formEncodingTypeAttribute = new FormEncodingTypeAttribute();

        [AttributeTypeAttributeMember(Name = "formmethod", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormMethodTypeAttribute _formMethodAttribute = new FormMethodTypeAttribute();

        [AttributeTypeAttributeMember(Name = "formnovalidate", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _formNoValidateAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "formtarget", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormTargetTypeAttribute _formTargetAttribute = new FormTargetTypeAttribute();

        [AttributeTypeAttributeMember(Name = "height", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly LengthTypeAttribute _heightAttribute = new LengthTypeAttribute();
      
        [AttributeTypeAttributeMember(Name = "list", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]     
        private readonly URITypeAttribute _listAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "max", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _maxAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "maxlength", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly NumberTypeAttribute _maxLenghtAttribute = new NumberTypeAttribute();

        [AttributeTypeAttributeMember(Name = "min", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _minAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "multiple", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _multipleAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "name", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _nameAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "pattern", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly PatternTypeAttribute _pattern = new PatternTypeAttribute();

        [AttributeTypeAttributeMember(Name = "placeholder", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _placeHolderAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "readonly", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagTypeAttribute _readonlyAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "required", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagTypeAttribute _requiredAttribute = new FlagTypeAttribute();

        [AttributeTypeAttributeMember(Name = "size", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NumberTypeAttribute _sizeAttribute = new NumberTypeAttribute();

        [AttributeTypeAttributeMember(Name = "src", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly URITypeAttribute _srcAttribute = new URITypeAttribute();

        [AttributeTypeAttributeMember(Name = "step", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly TextValueTypeAttribute _stepAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "type", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly InputTypeAttribute _inputTypeAttribute = new InputTypeAttribute();

        [AttributeTypeAttributeMember(Name = "value", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly TextValueTypeAttribute _valueAttribute = new TextValueTypeAttribute();

        [AttributeTypeAttributeMember(Name = "width", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly LengthTypeAttribute _widthAttribute = new LengthTypeAttribute();


        /// <summary>
        /// Alternate text for controls of the type image.
        /// </summary>
        public TextValueTypeAttribute Alt
        {
            get { return _altAttribute; }
        }

        /// <summary>
        /// Specifies whether an "input" element should have autocomplete enabled
        /// </summary>
        public OnOffTypeAttribute Autocomplete
        {
            get { return _autocompleteAttribute; }
        }

        /// <summary>
        /// Specifies that an "input" element should automatically get focus when the page loads
        /// </summary>
        public FlagTypeAttribute Autofocus
        {
            get { return _autoFocusAttribute; }
        }

        /// <summary>
        /// Form control name.
        /// </summary>
        public TextValueTypeAttribute Name
        {
            get { return _nameAttribute; }
        }

        /// <summary>
        /// Specifies a regular expression that an "input" element's value is checked against
        /// </summary>
        public PatternTypeAttribute Pattern { get { return _pattern; }}

        /// <summary>
        /// Specifies a short hint that describes the expected value of an "input" element
        /// </summary>
        public TextValueTypeAttribute Placeholder { get { return _placeHolderAttribute; }}

        /// <summary>
        /// When the type attribute has the value radio or checkbox, 
        /// this attribute specifies that the radio/checkbox is selected.
        /// </summary>
        public FlagTypeAttribute Checked
        {
            get { return _checkedAttribute; }
        }

        /// <summary>
        /// When the type attribute has the value text or password, this attribute specifies the maximum number of characters the user may enter. 
        /// This number should not exceed the value specified in the size attribute.
        /// </summary>
        public NumberTypeAttribute MaxLength
        {
            get { return _maxLenghtAttribute; }
        }

        /// <summary>
        /// Specifies a minimum value for an "input" element
        /// </summary>
        public TextValueTypeAttribute Min { get { return _minAttribute; }}


        /// <summary>
        /// Specifies that a user can enter more than one value in an "input" element
        /// </summary>
        public FlagTypeAttribute Multiple { get { return _multipleAttribute; }}

        /// <summary>
        /// This attribute tells the Web browser the initial width of the control. 
        /// The width is given in pixels except when the type attribute has the value text or password. 
        /// In such cases, its value is the number of characters.
        /// </summary>
        public NumberTypeAttribute Size
        {
            get { return _sizeAttribute; }
        }

        /// <summary>
        /// Type of control to create.
        /// </summary>
        public InputTypeAttribute InputType
        {
            get { return _inputTypeAttribute; }
        }


        /// <summary>
        /// This attribute specifies a comma-separated list of content types that a server processing this form will handle correctly.
        /// </summary>
        public ContentTypeAttribute Accept { get { return _acceptAttribute; } }



        /// <summary>
        ///  Specifies the alignment of an image input (only for type="image")
        /// Not supported in HTML5.
        /// </summary>
        public BiDirectionalAlignTypeAttribute Align { get { return _alignAttribute; }}


        /// <summary>
        /// Disables the control for user input. Possible value is disabled.
        /// </summary>
        public FlagTypeAttribute Disabled { get { return _disabledAttribute; } }


        /// <summary>
        /// Specifies one or more forms the "input" element belongs to
        /// </summary>
        public URITypeAttribute Form { get { return _formIdAttribute; }}


        /// <summary>
        /// Specifies the URL of the file that will process the input control when the form is submitted (for type="submit" and type="image")
        /// </summary>
        public URITypeAttribute FormAction { get { return _formActionAttribute; }}


        /// <summary>
        /// Specifies how the form-data should be encoded when submitting it to the server (for type="submit" and type="image")
        /// </summary>
        public FormEncodingTypeAttribute FormEncType { get { return _formEncodingTypeAttribute; }}


        /// <summary>
        /// Defines that form elements should not be validated when submitted
        /// </summary>
        public FlagTypeAttribute FormNoValidate { get { return _formNoValidateAttribute; }}

        /// <summary>
        /// Specifies where to display the response that is received after submitting the form (for type="submit" and type="image")
        /// </summary>
        public FormTargetTypeAttribute FormTarget { get { return _formTargetAttribute; }}

        /// <summary>
        /// Defines the HTTP method for sending data to the action URL (for type="submit" and type="image")
        /// </summary>
        public FormMethodTypeAttribute FormMethod { get { return _formMethodAttribute; }}

        /// <summary>
        /// Specifies the height of an "input" element (only for type="image")
        /// </summary>
        public LengthTypeAttribute Height { get { return _heightAttribute; }}

        /// <summary>
        /// Specifies the width of an "input" element (only for type="image")
        /// </summary>
        public LengthTypeAttribute Width { get { return _widthAttribute; }}


        /// <summary>
        /// Refers to a "datalist" element that contains pre-defined options for an "input" element
        /// </summary>
        public URITypeAttribute List { get { return _listAttribute; }} 

        /// <summary>
        /// Specifies the maximum value for an "input" element
        /// </summary>
        public TextValueTypeAttribute Max { get { return _maxAttribute; }}

        /// <summary>
        /// If present, this attribute prohibits changes to the value in the control. 
        /// Possible value is "readonly".
        /// </summary>
        public FlagTypeAttribute ReadOnly { get { return _readonlyAttribute; } }

        /// <summary>
        /// Specifies that an input field must be filled out before submitting the form
        /// </summary>
        public FlagTypeAttribute Required { get { return _requiredAttribute; }}

        /// <summary>
        /// When the type attribute has the value image, 
        /// this attribute specifies the location of the image to be used to decorate the graphical submit button.
        /// </summary>
        public URITypeAttribute Src { get { return _srcAttribute; } }

        /// <summary>
        /// Specifies the legal number intervals for an input field
        /// </summary>
        public TextValueTypeAttribute Step { get { return _stepAttribute; }}


        /// <summary>
        /// Value associated with a control.
        /// </summary>
        public TextValueTypeAttribute Value { get { return _valueAttribute; } }


        /// <summary>
        /// Checks it element data is valid
        /// </summary>
        /// <returns>
        /// true if valid
        /// </returns>
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
