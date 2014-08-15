using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using HTMLClassLibrary.Attributes;
using HTMLClassLibrary.Attributes.FlaggedAttributes;
using HTMLClassLibrary.BaseElements.InlineElements;

namespace HTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The button element is used to create button controls for forms. 
    /// Buttons created using the button element are similar in functionality to buttons created using the input element, 
    /// but offer greater rendering options.
    /// </summary>
    [HTMLItemAttribute(ElementName = "button", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Button : HTMLItem, IBlockElement
    {
        public Button()
        {
            RegisterAttribute(_type);
            RegisterAttribute(_name);
            RegisterAttribute(_value);
            RegisterAttribute(_disabled);
            RegisterAttribute(_autofocus);
            RegisterAttribute(_form);
            RegisterAttribute(_formAction);
            RegisterAttribute(_formEncoding);
            RegisterAttribute(_formMethod);
            RegisterAttribute(_formNoValidate);
            RegisterAttribute(_formTarget);
        }

        private readonly ButtonTypeAttribute _type = new ButtonTypeAttribute();
        private readonly NameAttribute      _name = new NameAttribute();
        private readonly ValueAttribute     _value = new ValueAttribute();
        private readonly DisabledAttribute  _disabled = new DisabledAttribute();
        private readonly AutoFocusAttribute _autofocus = new AutoFocusAttribute();
        private readonly FormIdAttribute _form = new FormIdAttribute();
        private readonly FormActionAttribute _formAction = new FormActionAttribute();
        private readonly FormEncodingTypeAttribute _formEncoding = new FormEncodingTypeAttribute();
        private readonly FormMethodAttribute _formMethod = new FormMethodAttribute();
        private readonly FormNoValidateAttribute _formNoValidate = new FormNoValidateAttribute();
        private readonly FormTargetAttribute _formTarget = new FormTargetAttribute();


        public ButtonTypeAttribute Type
        {
            get { return _type; }
        }

        public NameAttribute Name
        {
            get { return _name; }
        }

        public ValueAttribute Value
        {
            get { return _value; }
        }

        public DisabledAttribute Disabled
        {
            get { return _disabled; }
        }

        public AutoFocusAttribute Autofocus
        {
            get { return _autofocus; }
        }

        public FormIdAttribute Form
        {
            get { return _form; }
        }

        public FormActionAttribute FormAction
        {
            get { return _formAction; }
        }

        public FormEncodingTypeAttribute FormEncoding
        {
            get { return _formEncoding; }
        }

        public FormMethodAttribute FormMethod
        {
            get { return _formMethod; }
        }

        public FormNoValidateAttribute FormNoValidate
        {
            get { return _formNoValidate; }
        }

        public FormTargetAttribute FormTarget
        {
            get { return _formTarget; }
        }


        public override bool IsValid()
        {
            foreach (var item in Subitems)
            {
                if (!item.IsValid())
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool IsValidSubType(IHTMLItem item)
        {
            if (item is IInlineItem ||
                item is IBlockElement ||
                item is SimpleHTML5Text)
            {
                return item.IsValid();
            }
            return false;
        }
    }
}
