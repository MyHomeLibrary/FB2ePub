using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes
{
    public class MenuItemTypeAttribute : BaseAttribute
    {
        private enum MenuTypeEnum
        {
            Invalid,
            Checkbox,
            Command,
            Radio,
        }

        private MenuTypeEnum _buttonType = MenuTypeEnum.Invalid;

        private const string AttributeName = "type";
        private ContentType _attrObject = new ContentType();

        public override void AddAttribute(XElement xElement)
        {
            if (!_hasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            _hasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                _attrObject = new ContentType {Value = xObject.Value};
                _hasValue = true;
            }
        }

        public override string Value
        {
            get
            {
                switch (_buttonType)
                {
                    case MenuTypeEnum.Checkbox:
                        return "checkbox";
                    case MenuTypeEnum.Command:
                        return "command";
                    case MenuTypeEnum.Radio:
                        return "radio";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "checkbox":
                        _buttonType = MenuTypeEnum.Checkbox;
                        break;
                    case "command":
                        _buttonType = MenuTypeEnum.Command;
                        break;
                    case "radio":
                        _buttonType = MenuTypeEnum.Radio;
                        break;
                    default:
                        _buttonType = MenuTypeEnum.Invalid;
                        break;
                }
            }
        }


    }
}
