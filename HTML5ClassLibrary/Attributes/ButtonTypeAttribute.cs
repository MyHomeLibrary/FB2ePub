using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    public class ButtonTypeAttribute :BaseAttribute
    {
        private enum ButtonTypeEnum
        {
            Invalid,
            Submit,
            Reset,
            Button,
        }

        private ButtonTypeEnum _buttonType = ButtonTypeEnum.Invalid;
        private const string AttributeName = "type";
        private ContentType _attrObject = new ContentType();

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                _attrObject = new ContentType();
                _attrObject.Value = xObject.Value;
                AttributeHasValue = true;
            }
        }

        public override string Value
        {
            get
            {
                switch (_buttonType)
                {
                    case ButtonTypeEnum.Reset:
                        return "reset";
                    case ButtonTypeEnum.Button:
                        return "button";
                    case ButtonTypeEnum.Submit:
                        return "submit";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "reset":
                        _buttonType = ButtonTypeEnum.Reset;
                        break;
                    case "button":
                        _buttonType = ButtonTypeEnum.Button;
                        break;
                    case "submit":
                        _buttonType = ButtonTypeEnum.Submit;
                        break;
                    default:
                        _buttonType = ButtonTypeEnum.Invalid;
                        break;
                }
            }
        }

    }
}
