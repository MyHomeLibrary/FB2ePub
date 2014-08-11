using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// Events triggered by actions inside a HTML form (applies to almost all HTML elements, but is most used in form elements):
    /// </summary>
    public class FormEvents
    {
        private readonly OnBlurEventAttribute _onBlurEventAttribute = new OnBlurEventAttribute(); 
        private readonly OnChangeEventAttribute _onChangeEventAttribute = new OnChangeEventAttribute();  
        private readonly OnContextMenuAttribute _onContextMenuAttribute = new OnContextMenuAttribute();
        private readonly OnFocusEventAttribute _onFocusEventAttribute = new OnFocusEventAttribute(); 
        private readonly OnFormChangeEventAttribute _onFormChangeEventAttribute = new OnFormChangeEventAttribute();
        private readonly OnFormInputEventAttribute _onFormInputEventAttribute = new OnFormInputEventAttribute(); 
        private readonly OnInputEventAttribute _onInputEventAttribute = new OnInputEventAttribute();
        private readonly OnInvalidEventAttribute _onInvalidEventAttribute = new OnInvalidEventAttribute(); 
        private readonly OnSelectEventAttribute _onSelectEventAttribute = new OnSelectEventAttribute(); 
        private readonly OnSubmitEventAttribute _onSubmitEventAttribute = new OnSubmitEventAttribute(); 
        private readonly OnResetEventAttribute _onResetEventAttribute = new OnResetEventAttribute();
        private readonly OnToggleEventAttribute _onToggleEventAttribute = new OnToggleEventAttribute();




        /// <summary>
        /// Fires the moment that the element loses focus
        /// </summary>
        public OnBlurEventAttribute OnBlurEvent { get { return _onBlurEventAttribute; }}


        /// <summary>
        /// Fires the moment when the value of the element is changed
        /// </summary>
        public OnChangeEventAttribute OnChangeEvent { get { return _onChangeEventAttribute; }}


        /// <summary>
        /// Script to be run when a context menu is triggered
        /// </summary>
        public OnContextMenuAttribute OnContextMenu { get { return _onContextMenuAttribute; }}


        /// <summary>
        /// Fires the moment when the element gets focus
        /// </summary>
        public OnFocusEventAttribute OnFocusEvent { get { return _onFocusEventAttribute; }}


        /// <summary>
        /// Script to be run when a form changes
        /// </summary>
        public OnFormChangeEventAttribute OnFormChangeEvent { get { return _onFormChangeEventAttribute; }}


        /// <summary>
        /// Script to be run when a form gets user input
        /// </summary>
        public OnFormInputEventAttribute OnFormInputEvent { get { return _onFormInputEventAttribute; }}


        /// <summary>
        /// Script to be run when an element gets user input
        /// </summary>
        public OnInputEventAttribute OnInputEvent { get { return _onInputEventAttribute; }}


        /// <summary>
        /// Script to be run when an element is invalid
        /// </summary>
        public OnInvalidEventAttribute OnInvalidEvent { get { return _onInvalidEventAttribute; }}


        /// <summary>
        /// Fires after some text has been selected in an element
        /// </summary>
        public OnSelectEventAttribute OnSelectEvent { get { return _onSelectEventAttribute; }}


        /// <summary>
        /// Fires when a form is submitted
        /// </summary>
        public OnSubmitEventAttribute OnSubmitEvent { get { return _onSubmitEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnResetEventAttribute OnResetEvent { get { return _onResetEventAttribute; } }

        /// <summary>
        /// 
        /// </summary>
        public  OnToggleEventAttribute OnToggleEvent { get { return _onToggleEventAttribute; }}


        /// <summary>
        /// Add all attributes set to specified xElement
        /// </summary>
        /// <param name="xElement">element to store/write attributes to</param>
        public void AddAttributes(XElement xElement)
        {
            _onBlurEventAttribute.AddAttribute(xElement);
            _onChangeEventAttribute.AddAttribute(xElement);
            _onContextMenuAttribute.AddAttribute(xElement);
            _onFocusEventAttribute.AddAttribute(xElement);
            _onFormChangeEventAttribute.AddAttribute(xElement);
            _onFormInputEventAttribute.AddAttribute(xElement);
            _onInputEventAttribute.AddAttribute(xElement);
            _onInvalidEventAttribute.AddAttribute(xElement);
            _onSelectEventAttribute.AddAttribute(xElement);
            _onSubmitEventAttribute.AddAttribute(xElement);
            _onResetEventAttribute.AddAttribute(xElement);
            _onToggleEventAttribute.AddAttribute(xElement);
        }

        /// <summary>
        /// Loads all attributes from provided xElement
        /// </summary>
        /// <param name="xElement">element to load attributes from</param>
        public void ReadAttributes(XElement xElement)
        {
            _onBlurEventAttribute.ReadAttribute(xElement);
            _onChangeEventAttribute.ReadAttribute(xElement);
            _onContextMenuAttribute.ReadAttribute(xElement);
            _onFocusEventAttribute.ReadAttribute(xElement);
            _onFormChangeEventAttribute.ReadAttribute(xElement);
            _onFormInputEventAttribute.ReadAttribute(xElement);
            _onInputEventAttribute.ReadAttribute(xElement);
            _onInvalidEventAttribute.ReadAttribute(xElement);
            _onSelectEventAttribute.ReadAttribute(xElement);
            _onSubmitEventAttribute.ReadAttribute(xElement);
            _onResetEventAttribute.ReadAttribute(xElement);
            _onToggleEventAttribute.ReadAttribute(xElement);
        }
    }
}
