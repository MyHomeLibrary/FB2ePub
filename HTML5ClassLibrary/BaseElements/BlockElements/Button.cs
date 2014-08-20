using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using XHTMLClassLibrary.Attributes;
using XHTMLClassLibrary.Attributes.FlaggedAttributes;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace XHTMLClassLibrary.BaseElements.BlockElements
{
    /// <summary>
    /// The button element is used to create button controls for forms. 
    /// Buttons created using the button element are similar in functionality to buttons created using the input element, 
    /// but offer greater rendering options.
    /// </summary>
    [HTMLItemAttribute(ElementName = "button", SupportedStandards = HTMLElementType.HTML5 |  HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
    public class Button : HTMLItem, IBlockElement
    {
        [AttributeTypeAttributeMember(Name = "autofocus", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FlagAttribute _autofocus = new FlagAttribute();

        [AttributeTypeAttributeMember(Name = "disabled", SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly FlagAttribute _disabled = new FlagAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormIdAttribute _form = new FormIdAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormActionAttribute _formAction = new FormActionAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormEncodingTypeAttribute _formEncoding = new FormEncodingTypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormMethodAttribute _formMethod = new FormMethodAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormNoValidateAttribute _formNoValidate = new FormNoValidateAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5)]
        private readonly FormTargetAttribute _formTarget = new FormTargetAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly NameAttribute _name = new NameAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ButtonTypeAttribute _type = new ButtonTypeAttribute();

        [AttributeTypeAttributeMember(SupportedStandards = HTMLElementType.HTML5 | HTMLElementType.XHTML5 | HTMLElementType.XHTML11 | HTMLElementType.Transitional | HTMLElementType.Strict | HTMLElementType.FrameSet)]
        private readonly ValueAttribute     _value = new ValueAttribute();



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

        public FlagAttribute Disabled
        {
            get { return _disabled; }
        }

        public FlagAttribute Autofocus
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
