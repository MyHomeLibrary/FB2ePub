using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
using HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal;
using HTML5ClassLibrary.Attributes.AttributeGroups.KeyboardEvents;
using HTML5ClassLibrary.Attributes.AttributeGroups.MouseEvents;
using HTML5ClassLibrary.Attributes.Events;
using HTML5ClassLibrary.Attributes.FlaggedAttributes;

namespace HTML5ClassLibrary.BaseElements.InlineElements
{
    /// <summary>
    /// The "input" tag specifies an input field where the user can enter data.
    /// "input" elements are used within a "form" element to declare input controls that allow users to input data.
    /// An input field can vary in many ways, depending on the type attribute.
    /// </summary>
    public class Input : BaseInlineItem
    {
        public Input()
        {
            Attributes.Add(_altAttribute);
            Attributes.Add(_autocompleteAttribute);
            Attributes.Add(_autoFocusAttribute);
            Attributes.Add(_nameAttribute);
            Attributes.Add(_pattern);
            Attributes.Add(_placeHolderAttribute);
            Attributes.Add(_checkedAttribute);
            Attributes.Add(_maxLenghtAttribute);
            Attributes.Add(_minAttribute);
            Attributes.Add(_multipleAttribute);
            Attributes.Add(_sizeAttribute);
            Attributes.Add(_inputTypeAttribute);
            Attributes.Add(_valueAttribute);
            Attributes.Add(_acceptAttribute);
            Attributes.Add(_disabledAttribute);
            Attributes.Add(_formIdAttribute);
            Attributes.Add(_formActionAttribute);
            Attributes.Add(_formEncodingTypeAttribute);
            Attributes.Add(_formNoValidateAttribute);
            Attributes.Add(_formTargetAttribute);
            Attributes.Add(_formMethodAttribute);
            Attributes.Add(_heightAttribute);
            Attributes.Add(_widthAttribute);
            Attributes.Add(_listAttribute);
            Attributes.Add(_maxAttribute);
            Attributes.Add(_isMapAttribute);
            Attributes.Add(_onChange);
            Attributes.Add(_onSelect);
            Attributes.Add(_readonlyAttribute);
            Attributes.Add(_requiredAttribute);
            Attributes.Add(_srcAttribute);
            Attributes.Add(_stepAttribute);
            Attributes.Add(_useMapAttribute);
            
        }

        private readonly AcceptAttribute _acceptAttribute = new AcceptAttribute();
        private readonly AltAttribute _altAttribute = new AltAttribute();
        private readonly AutocompleteAttribute _autocompleteAttribute = new AutocompleteAttribute();
        private readonly AutoFocusAttribute _autoFocusAttribute = new AutoFocusAttribute();
        private readonly CheckedAttribute _checkedAttribute = new CheckedAttribute();
        private readonly DisabledAttribute _disabledAttribute = new DisabledAttribute();
        private readonly FormIdAttribute _formIdAttribute = new FormIdAttribute();
        private readonly FormActionAttribute _formActionAttribute = new FormActionAttribute();
        private readonly FormEncodingTypeAttribute _formEncodingTypeAttribute = new FormEncodingTypeAttribute();
        private readonly FormNoValidateAttribute _formNoValidateAttribute = new FormNoValidateAttribute();
        private readonly FormTargetAttribute _formTargetAttribute = new FormTargetAttribute();
        private readonly FormMethodAttribute _formMethodAttribute = new FormMethodAttribute();
        private readonly HeightAttribute _heightAttribute = new HeightAttribute();
        private readonly WidthAttribute _widthAttribute = new WidthAttribute();   
        private readonly ListAttribute _listAttribute = new ListAttribute();
        private readonly MaxAttribute _maxAttribute = new MaxAttribute();
        private readonly MaxLengthAttribute _maxLenghtAttribute = new MaxLengthAttribute();
        private readonly MinAttribute _minAttribute = new MinAttribute();
        private readonly MultipleAttribute _multipleAttribute = new MultipleAttribute();
        private readonly NameAttribute _nameAttribute = new NameAttribute();
        private readonly PatternAttribute _pattern = new PatternAttribute();
        private readonly PlaceHolderAttribute _placeHolderAttribute = new PlaceHolderAttribute();
        private readonly ReadOnlyAttribute _readonlyAttribute = new ReadOnlyAttribute();
        private readonly RequiredAttribute _requiredAttribute = new RequiredAttribute();
        private readonly SizeAttribute _sizeAttribute = new SizeAttribute();
        private readonly SourceAttribute _srcAttribute = new SourceAttribute();
        private readonly StepAttribute _stepAttribute = new StepAttribute();
        private readonly InputTypeAttribute _inputTypeAttribute = new InputTypeAttribute();
        private readonly ValueAttribute _valueAttribute = new ValueAttribute();
        private readonly ISMapAttribute _isMapAttribute = new ISMapAttribute();
        private readonly OnChangeEventAttribute _onChange = new OnChangeEventAttribute();
        private readonly OnSelectEventAttribute _onSelect = new OnSelectEventAttribute();
        private readonly UseMapAttribute _useMapAttribute = new UseMapAttribute();


        public const string ElementName = "input";


        /// <summary>
        /// Alternate text for controls of the type image.
        /// </summary>
        public AltAttribute Alt
        {
            get { return _altAttribute; }
        }

        /// <summary>
        /// Specifies whether an "input" element should have autocomplete enabled
        /// </summary>
        public AutocompleteAttribute Autocomplete
        {
            get { return _autocompleteAttribute; }
        }

        /// <summary>
        /// Specifies that an "input" element should automatically get focus when the page loads
        /// </summary>
        public AutoFocusAttribute Autofocus
        {
            get { return _autoFocusAttribute; }
        }

        /// <summary>
        /// Form control name.
        /// </summary>
        public NameAttribute Name
        {
            get { return _nameAttribute; }
        }

        /// <summary>
        /// Specifies a regular expression that an "input" element's value is checked against
        /// </summary>
        public PatternAttribute Pattern { get { return _pattern; }}

        /// <summary>
        /// Specifies a short hint that describes the expected value of an "input" element
        /// </summary>
        public PlaceHolderAttribute Placeholder { get { return _placeHolderAttribute; }}

        /// <summary>
        /// When the type attribute has the value radio or checkbox, 
        /// this attribute specifies that the radio/checkbox is selected.
        /// </summary>
        public CheckedAttribute Checked
        {
            get { return _checkedAttribute; }
        }

        /// <summary>
        /// When the type attribute has the value text or password, this attribute specifies the maximum number of characters the user may enter. 
        /// This number should not exceed the value specified in the size attribute.
        /// </summary>
        public MaxLengthAttribute MaxLength
        {
            get { return _maxLenghtAttribute; }
        }

        /// <summary>
        /// Specifies a minimum value for an "input" element
        /// </summary>
        public MinAttribute Min { get { return _minAttribute; }}


        /// <summary>
        /// Specifies that a user can enter more than one value in an "input" element
        /// </summary>
        public MultipleAttribute Multiple { get { return _multipleAttribute; }}

        /// <summary>
        /// This attribute tells the Web browser the initial width of the control. 
        /// The width is given in pixels except when the type attribute has the value text or password. 
        /// In such cases, its value is the number of characters.
        /// </summary>
        public SizeAttribute Size
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
        public AcceptAttribute Accept { get { return _acceptAttribute; } }


        /// <summary>
        /// Disables the control for user input. Possible value is disabled.
        /// </summary>
        public DisabledAttribute Disabled { get { return _disabledAttribute; } }


        /// <summary>
        /// Specifies one or more forms the "input" element belongs to
        /// </summary>
        public FormIdAttribute Form { get { return _formIdAttribute; }}


        /// <summary>
        /// Specifies the URL of the file that will process the input control when the form is submitted (for type="submit" and type="image")
        /// </summary>
        public FormActionAttribute FormAction { get { return _formActionAttribute; }}


        /// <summary>
        /// Specifies how the form-data should be encoded when submitting it to the server (for type="submit" and type="image")
        /// </summary>
        public FormEncodingTypeAttribute FormEncType { get { return _formEncodingTypeAttribute; }}


        /// <summary>
        /// Defines that form elements should not be validated when submitted
        /// </summary>
        public FormNoValidateAttribute FormNoValidate { get { return _formNoValidateAttribute; }}

        /// <summary>
        /// Specifies where to display the response that is received after submitting the form (for type="submit" and type="image")
        /// </summary>
        public FormTargetAttribute FormTarget { get { return _formTargetAttribute; }}

        /// <summary>
        /// Defines the HTTP method for sending data to the action URL (for type="submit" and type="image")
        /// </summary>
        public FormMethodAttribute FormMethod { get { return _formMethodAttribute; }}

        /// <summary>
        /// Specifies the height of an "input" element (only for type="image")
        /// </summary>
        public HeightAttribute Height { get { return _heightAttribute; }}

        /// <summary>
        /// Specifies the width of an "input" element (only for type="image")
        /// </summary>
        public WidthAttribute Width { get { return _widthAttribute; }}


        /// <summary>
        /// Refers to a "datalist" element that contains pre-defined options for an "input" element
        /// </summary>
        public ListAttribute List { get { return _listAttribute; }} 

        /// <summary>
        /// Specifies the maximum value for an "input" element
        /// </summary>
        public MaxAttribute Max { get { return _maxAttribute; }}

        /// <summary>
        /// If present, this attribute specifies that a server-side image map should be used. Possible value is ismap.
        /// </summary>
        public ISMapAttribute ISMap { get { return _isMapAttribute; } }

        /// <summary>
        /// A client-side script event that occurs when a control loses the input focus and its value is modified prior
        /// to its next receiving focus
        /// </summary>
        public OnChangeEventAttribute OnChange { get { return _onChange; } }

        /// <summary>
        /// A client-side script event that occurs when a user selects some text in a text field.
        /// </summary>
        public OnSelectEventAttribute OnSelect { get { return _onSelect; } }

        /// <summary>
        /// If present, this attribute prohibits changes to the value in the control. 
        /// Possible value is "readonly".
        /// </summary>
        public ReadOnlyAttribute ReadOnly { get { return _readonlyAttribute; } }

        /// <summary>
        /// Specifies that an input field must be filled out before submitting the form
        /// </summary>
        public RequiredAttribute Required { get { return _requiredAttribute; }}

        /// <summary>
        /// When the type attribute has the value image, 
        /// this attribute specifies the location of the image to be used to decorate the graphical submit button.
        /// </summary>
        public SourceAttribute Src { get { return _srcAttribute; } }

        /// <summary>
        /// Specifies the legal number intervals for an input field
        /// </summary>
        public StepAttribute Step { get { return _stepAttribute; }}

        /// <summary>
        /// When the type attribute has the value image, this attribute associates the image to a client-side image map defined by a map element. 
        /// The value of this attribute must match the id attribute of the map element.
        /// </summary>
        public UseMapAttribute UseMap { get { return _useMapAttribute; } }


        /// <summary>
        /// Value associated with a control.
        /// </summary>
        public ValueAttribute Value { get { return _valueAttribute; } }

        #region Overrides of BaseInlineItem

        /// <summary>
        /// Loads the element from XNode
        /// </summary>
        /// <param name="xNode">node to load element from</param>
        public override void Load(XNode xNode)
        {
            if (xNode.NodeType != XmlNodeType.Element)
            {
                throw new Exception("xNode is not of element type");
            }
            var xElement = (XElement)xNode;
            if (xElement.Name.LocalName != ElementName)
            {
                throw new Exception(string.Format("xNode is not {0} element", ElementName));
            }

            ReadAttributes(xElement);
        }

        /// <summary>
        /// Generates element to XNode from data
        /// </summary>
        /// <returns>
        /// generated XNode
        /// </returns>
        public override XNode Generate()
        {
            var xElement = new XElement(XhtmlNameSpace + ElementName);

            AddAttributes(xElement);

            return xElement;

        }

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

        /// <summary>
        /// Adds sub-item to the item , only if 
        /// allowed by the rules and element can accept content
        /// </summary>
        /// <param name="item">sub-item to add</param>
        public override void Add(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override void Remove(IHTML5Item item)
        {
            throw new Exception("This element does not contain sub-items");
        }

        public override List<IHTML5Item> SubElements()
        {
            return null;
        }
        #endregion
    }
}
