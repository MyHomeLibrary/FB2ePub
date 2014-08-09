using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class MenuTypeAttribute : BaseAttribute
    {
        private enum MenuType
        {
            Invalid,
            Popup,
            Toolbar,
            Context,
        }

        private MenuType _type = MenuType.Invalid;

        private const string AttributeName = "type";

        public override string Value
        {
            get
            {
                switch (_type)
                {
                    case MenuType.Popup:
                        return "popup";
                    case MenuType.Toolbar:
                        return "toolbar";
                    case MenuType.Context:
                        return "context";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "popup":
                        _type = MenuType.Popup;
                        break;
                    case "toolbar":
                        _type = MenuType.Toolbar;
                        break;
                    case "context":
                        _type = MenuType.Context;
                        break;
                    default:
                        _type = MenuType.Invalid;
                        break;
                }
            }
        }



        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!_hasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, Value));
        }

        public override void ReadAttribute(XElement element)
        {
            _hasValue = false;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }

        #endregion


    }
}
