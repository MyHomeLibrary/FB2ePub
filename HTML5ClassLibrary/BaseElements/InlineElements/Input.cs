using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.Attributes;
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

        private readonly LanguageAttr _language = new LanguageAttr();
        private readonly DirectionAttr _direction = new DirectionAttr();

        // basic attributes
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

        // Advanced attributes
        private readonly AccessKeyAttribute _accessKeyAttrib = new AccessKeyAttribute();
        private readonly ISMapAttribute _isMapAttribute = new ISMapAttribute();
        private readonly OnBlurEventAttribute _onBlurEvent = new OnBlurEventAttribute();
        private readonly OnChangeEventAttribute _onChange = new OnChangeEventAttribute();
        private readonly OnFocusEventAttribute _onFocus = new OnFocusEventAttribute();
        private readonly OnSelectEventAttribute _onSelect = new OnSelectEventAttribute();
        private readonly TabIndexAttribute _tabIndexAttrib = new TabIndexAttribute();
        private readonly UseMapAttribute _useMapAttribute = new UseMapAttribute();

        // Common event attributes
        private readonly OnClickEventAttribute _onClick = new OnClickEventAttribute();
        private readonly OnDblClickEventAttribute _onDblClick = new OnDblClickEventAttribute();
        private readonly OnMouseDownEventAttribute _onMouseDown = new OnMouseDownEventAttribute();
        private readonly OnMouseUpEventAttribute _onMouseUp = new OnMouseUpEventAttribute();
        private readonly OnMouseOverEventAttribute _onMouseOver = new OnMouseOverEventAttribute();
        private readonly OnMouseMoveEventAttribute _onMouseMove = new OnMouseMoveEventAttribute();
        private readonly OnMouseOutEventAttribute _onMouseOut = new OnMouseOutEventAttribute();
        private readonly OnKeyPressEventAttribute _onKeyPress = new OnKeyPressEventAttribute();
        private readonly OnKeyDownEventAttribute _onKeyDown = new OnKeyDownEventAttribute();
        private readonly OnKeyUpEventAttribute _onKeyUp = new OnKeyUpEventAttribute();


        public const string ElementName = "input";

        /// <summary>
        /// This attribute specifies the base direction of text. 
        /// Possible values:
        /// ltr: Left-to-right 
        /// rtl: Right-to-left
        /// </summary>
        public DirectionAttr Direction
        {
            get { return _direction; }
        }

        /// <summary>
        /// A client-side script event that occurs when a pointing device button is clicked over an element.
        /// </summary>
        public OnClickEventAttribute OnClick
        {
            get { return _onClick; }
        }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is double-clicked over an element.
        /// </summary>
        public OnDblClickEventAttribute OnDblClick { get { return _onDblClick; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device button is pressed down over an element.
        /// </summary>
        public OnMouseDownEventAttribute OnMouseDown { get { return _onMouseDown; } }

        /// <summary>
        /// A client-side script event that occurs when a pointing device button is released over an element.
        /// </summary>
        public OnMouseUpEventAttribute OnMouseUp { get { return _onMouseUp; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved onto an element.
        /// </summary>
        public OnMouseOverEventAttribute OnMouseOver { get { return _onMouseOver; } }

        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved within an element.
        /// </summary>
        public OnMouseMoveEventAttribute OnMouseMove { get { return _onMouseMove; } }


        /// <summary>
        /// A client-side script event that occurs when a pointing device is moved away from an element.
        /// </summary>
        public OnMouseOutEventAttribute OnMouseOut { get { return _onMouseOut; } }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element then released.
        /// </summary>
        public OnKeyPressEventAttribute OnKeyPress { get { return _onKeyPress; } }

        /// <summary>
        /// A client-side script event that occurs when a key is pressed down over an element.
        /// </summary>
        public OnKeyDownEventAttribute OnKeyDown { get { return _onKeyDown; } }

        /// <summary>
        /// A client-side script event that occurs when a key is released over an element.
        /// </summary>
        public OnKeyUpEventAttribute OnKeyUp { get { return _onKeyUp; } }

        /// <summary>
        /// This attribute specifies the base language of an element's attribute values and text content.
        /// </summary>
        public LanguageAttr Language
        {
            get { return _language; }
        }

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
        /// Accessibility key character
        /// </summary>
        public AccessKeyAttribute AccessKey { get { return _accessKeyAttrib; } }

        /// <summary>
        /// A client-side script event that occurs when an element loses focus either by the pointing device or by tabbing navigation.
        /// </summary>
        public OnBlurEventAttribute OnBlur { get { return _onBlurEvent; } }

        /// <summary>
        /// A client-side script event that occurs when an element receives focus either by the pointing device or by tabbing navigation.
        /// </summary>
        public OnFocusEventAttribute OnFocus { get { return _onFocus; } }


        /// <summary>
        /// Position in tabbing order.
        /// </summary>
        public TabIndexAttribute TabIndex { get { return _tabIndexAttrib; } }


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

        protected override void AddAttributes(XElement xElement)
        {
            base.AddAttributes(xElement);
            _altAttribute.AddAttribute(xElement);
            _autocompleteAttribute.AddAttribute(xElement);
            _autoFocusAttribute.AddAttribute(xElement);
            _nameAttribute.AddAttribute(xElement);
            _pattern.AddAttribute(xElement);
            _placeHolderAttribute.AddAttribute(xElement);
            _checkedAttribute.AddAttribute(xElement);
            _maxLenghtAttribute.AddAttribute(xElement);
            _minAttribute.AddAttribute(xElement);
            _multipleAttribute.AddAttribute(xElement);
            _sizeAttribute.AddAttribute(xElement);
            _inputTypeAttribute.AddAttribute(xElement);
            _valueAttribute.AddAttribute(xElement);


            _acceptAttribute.AddAttribute(xElement);
            _accessKeyAttrib.AddAttribute(xElement);
            _disabledAttribute.AddAttribute(xElement);
            _formIdAttribute.AddAttribute(xElement);
            _formActionAttribute.AddAttribute(xElement);
            _formEncodingTypeAttribute.AddAttribute(xElement);
            _formNoValidateAttribute.AddAttribute(xElement);
            _formTargetAttribute.AddAttribute((xElement));
            _formMethodAttribute.AddAttribute(xElement);
            _heightAttribute.AddAttribute(xElement);
            _widthAttribute.AddAttribute(xElement);
            _listAttribute.AddAttribute(xElement);
            _maxAttribute.AddAttribute(xElement);
            _isMapAttribute.AddAttribute(xElement);
            _onBlurEvent.AddAttribute(xElement);
            _onChange.AddAttribute(xElement);
            _onFocus.AddAttribute(xElement);
            _onSelect.AddAttribute(xElement);
            _readonlyAttribute.AddAttribute(xElement);
            _requiredAttribute.AddAttribute(xElement);
            _srcAttribute.AddAttribute(xElement);
            _stepAttribute.AddAttribute(xElement);
            _tabIndexAttrib.AddAttribute(xElement);
            _useMapAttribute.AddAttribute(xElement);

            _language.AddAttribute(xElement);
            _direction.AddAttribute(xElement);

            _onClick.AddAttribute(xElement);
            _onDblClick.AddAttribute(xElement);
            _onMouseDown.AddAttribute(xElement);
            _onMouseUp.AddAttribute(xElement);
            _onMouseOver.AddAttribute(xElement);
            _onMouseMove.AddAttribute(xElement);
            _onMouseOut.AddAttribute(xElement);
            _onKeyPress.AddAttribute(xElement);
            _onKeyDown.AddAttribute(xElement);
            _onKeyUp.AddAttribute(xElement);
        }


        protected override void ReadAttributes(XElement xElement)
        {
            base.ReadAttributes(xElement);
            _altAttribute.ReadAttribute(xElement);
            _autocompleteAttribute.ReadAttribute(xElement);
            _autoFocusAttribute.ReadAttribute(xElement);
            _nameAttribute.ReadAttribute(xElement);
            _pattern.ReadAttribute(xElement);
            _placeHolderAttribute.ReadAttribute(xElement);
            _checkedAttribute.ReadAttribute(xElement);
            _maxLenghtAttribute.ReadAttribute(xElement);
            _minAttribute.ReadAttribute(xElement);
            _multipleAttribute.ReadAttribute(xElement);
            _sizeAttribute.ReadAttribute(xElement);
            _inputTypeAttribute.ReadAttribute(xElement);
            _valueAttribute.ReadAttribute(xElement);

            _acceptAttribute.ReadAttribute(xElement);
            _accessKeyAttrib.ReadAttribute(xElement);
            _disabledAttribute.ReadAttribute(xElement);
            _formIdAttribute.ReadAttribute(xElement);
            _formActionAttribute.ReadAttribute(xElement);
            _formEncodingTypeAttribute.ReadAttribute(xElement);
            _formNoValidateAttribute.ReadAttribute(xElement);
            _formTargetAttribute.ReadAttribute(xElement);
            _formMethodAttribute.ReadAttribute(xElement);
            _heightAttribute.ReadAttribute(xElement);
            _widthAttribute.ReadAttribute(xElement);
            _listAttribute.ReadAttribute(xElement);
            _maxAttribute.ReadAttribute(xElement);
            _isMapAttribute.ReadAttribute(xElement);
            _onBlurEvent.ReadAttribute(xElement);
            _onChange.ReadAttribute(xElement);
            _onFocus.ReadAttribute(xElement);
            _onSelect.ReadAttribute(xElement);
            _readonlyAttribute.ReadAttribute(xElement);
            _requiredAttribute.ReadAttribute(xElement);
            _srcAttribute.ReadAttribute(xElement);
            _stepAttribute.ReadAttribute(xElement);
            _tabIndexAttrib.ReadAttribute(xElement);
            _useMapAttribute.ReadAttribute(xElement);


            _language.ReadAttribute(xElement);
            _direction.ReadAttribute(xElement);

            _onClick.ReadAttribute(xElement);
            _onDblClick.ReadAttribute(xElement);
            _onMouseDown.ReadAttribute(xElement);
            _onMouseUp.ReadAttribute(xElement);
            _onMouseOver.ReadAttribute(xElement);
            _onMouseMove.ReadAttribute(xElement);
            _onMouseOut.ReadAttribute(xElement);
            _onKeyPress.ReadAttribute(xElement);
            _onKeyDown.ReadAttribute(xElement);
            _onKeyUp.ReadAttribute(xElement);

        }

        #endregion
    }
}
