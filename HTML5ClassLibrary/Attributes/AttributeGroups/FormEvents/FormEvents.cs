using System.Collections.Generic;
using XHTMLClassLibrary.BaseElements;

namespace XHTMLClassLibrary.Attributes.AttributeGroups.FormEvents
{
    /// <summary>
    /// Events triggered by actions inside a HTML form (applies to almost all HTML elements, but is most used in form elements):
    /// </summary>
    public class FormEvents
    {
        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnBlurEventAttribute _onBlurEventAttribute = new OnBlurEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnChangeEventAttribute _onChangeEventAttribute = new OnChangeEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnContextMenuAttribute _onContextMenuAttribute = new OnContextMenuAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnFocusEventAttribute _onFocusEventAttribute = new OnFocusEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnFormChangeEventAttribute _onFormChangeEventAttribute = new OnFormChangeEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnFormInputEventAttribute _onFormInputEventAttribute = new OnFormInputEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnInputEventAttribute _onInputEventAttribute = new OnInputEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly OnInvalidEventAttribute _onInvalidEventAttribute = new OnInvalidEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnSelectEventAttribute _onSelectEventAttribute = new OnSelectEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnSubmitEventAttribute _onSubmitEventAttribute = new OnSubmitEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly OnResetEventAttribute _onResetEventAttribute = new OnResetEventAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
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

    }
}
