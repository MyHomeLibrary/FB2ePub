using XHTMLClassLibrary.Attributes.Events;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups
{
    /// <summary>
    /// Events triggered by actions inside a HTML form (applies to almost all HTML elements, but is most used in form elements):
    /// </summary>
    public class FormEvents
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onBlurEventAttribute = new OnEventAttribute("onblur");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onChangeEventAttribute = new OnEventAttribute("onchange");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onContextMenuAttribute = new OnEventAttribute("oncontextmenu");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onFocusEventAttribute = new OnEventAttribute("onfocus");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onFormChangeEventAttribute = new OnEventAttribute("onformchange");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onFormInputEventAttribute = new OnEventAttribute("onforminput");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onInputEventAttribute = new OnEventAttribute("oninput");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onInvalidEventAttribute = new OnEventAttribute("oninvalid");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onSelectEventAttribute = new OnEventAttribute("onselect");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onSubmitEventAttribute = new OnEventAttribute("onsubmit");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnEventAttribute _onResetEventAttribute = new OnEventAttribute("onreset");

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnEventAttribute _onToggleEventAttribute = new OnEventAttribute("ontogle");




        /// <summary>
        /// Fires the moment that the element loses focus
        /// </summary>
        public OnEventAttribute OnBlurEvent { get { return _onBlurEventAttribute; }}


        /// <summary>
        /// Fires the moment when the value of the element is changed
        /// </summary>
        public OnEventAttribute OnChangeEvent { get { return _onChangeEventAttribute; }}


        /// <summary>
        /// Script to be run when a context menu is triggered
        /// </summary>
        public OnEventAttribute OnContextMenu { get { return _onContextMenuAttribute; }}


        /// <summary>
        /// Fires the moment when the element gets focus
        /// </summary>
        public OnEventAttribute OnFocusEvent { get { return _onFocusEventAttribute; }}


        /// <summary>
        /// Script to be run when a form changes
        /// </summary>
        public OnEventAttribute OnFormChangeEvent { get { return _onFormChangeEventAttribute; }}


        /// <summary>
        /// Script to be run when a form gets user input
        /// </summary>
        public OnEventAttribute OnFormInputEvent { get { return _onFormInputEventAttribute; }}


        /// <summary>
        /// Script to be run when an element gets user input
        /// </summary>
        public OnEventAttribute OnInputEvent { get { return _onInputEventAttribute; }}


        /// <summary>
        /// Script to be run when an element is invalid
        /// </summary>
        public OnEventAttribute OnInvalidEvent { get { return _onInvalidEventAttribute; }}


        /// <summary>
        /// Fires after some text has been selected in an element
        /// </summary>
        public OnEventAttribute OnSelectEvent { get { return _onSelectEventAttribute; }}


        /// <summary>
        /// Fires when a form is submitted
        /// </summary>
        public OnEventAttribute OnSubmitEvent { get { return _onSubmitEventAttribute; }}

        /// <summary>
        /// 
        /// </summary>
        public OnEventAttribute OnResetEvent { get { return _onResetEventAttribute; } }

        /// <summary>
        /// 
        /// </summary>
        public  OnEventAttribute OnToggleEvent { get { return _onToggleEventAttribute; }}

    }
}
