using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class InputTypeAttribute : BaseAttribute
    {
        private enum InputTypeEnum
        {
            Invalid,
            Button,
            CheckBox,
            Color,
            Date,
            DateTime,
            DateTimeLocal,
            EMail,
            File,
            Hidden,
            Image,
            Month,
            Number,
            Password,
            Radio,
            Range,
            Reset,
            Search,
            Submit,
            Tel,
            Text,
            Time,
            URL,
            Week,
        }

        private InputTypeEnum _type = InputTypeEnum.Invalid;

        private const string AttributeName = "type";

        public override string Value
        {
            get
            {
                switch (_type)
                {
                    case InputTypeEnum.Password:
                        return "password";
                    case InputTypeEnum.Radio:
                        return "radio";
                    case InputTypeEnum.CheckBox:
                        return "checkbox";
                    case InputTypeEnum.Text:
                        return "text";
                    case InputTypeEnum.Submit:
                        return "submit";
                    case InputTypeEnum.Image:
                        return "image";
                    case InputTypeEnum.Reset:
                        return "reset";
                    case InputTypeEnum.Button:
                        return "button";
                    case InputTypeEnum.Hidden:
                        return "hidden";
                    case InputTypeEnum.File:
                        return "file";
                    case InputTypeEnum.Color:
                        return "color";
                    case InputTypeEnum.Date:
                        return "date";
                    case InputTypeEnum.DateTime:
                        return "datetime";
                    case InputTypeEnum.DateTimeLocal:
                        return "datetime-local";
                    case InputTypeEnum.EMail:
                        return "email";
                    case InputTypeEnum.Month:
                        return "month";
                    case InputTypeEnum.Number:
                        return "number";
                    case InputTypeEnum.Range:
                        return "range";
                    case InputTypeEnum.Search:
                        return "search";
                    case InputTypeEnum.Tel:
                        return "tel";
                    case InputTypeEnum.Time:
                        return "time";
                    case InputTypeEnum.URL:
                        return "url";
                    case InputTypeEnum.Week:
                        return "week";
                }
                return string.Empty;
            }

            set
            {
                AttributeHasValue = true;
                switch (value.ToLower())
                {
                    case "password":
                        _type = InputTypeEnum.Password;
                        break;
                    case "radio":
                        _type = InputTypeEnum.Radio;
                        break;
                    case "checkbox":
                        _type = InputTypeEnum.CheckBox;
                        break;
                    case "text":
                        _type = InputTypeEnum.Text;
                        break;
                    case "submit":
                        _type = InputTypeEnum.Submit;
                        break;
                    case "image":
                        _type = InputTypeEnum.Image;
                        break;
                    case "reset":
                        _type =  InputTypeEnum.Reset;
                        break;
                    case "button":
                        _type = InputTypeEnum.Button;
                        break;
                    case "hidden":
                        _type = InputTypeEnum.Hidden;
                        break;
                    case "file":
                        _type = InputTypeEnum.File;
                        break;
                    case "color":
                        _type = InputTypeEnum.Color;
                        break;
                    case "date":
                        _type = InputTypeEnum.Date;
                        break;
                    case "datetime":
                        _type = InputTypeEnum.DateTime;
                        break;
                    case "datetime-local":
                        _type = InputTypeEnum.DateTimeLocal;
                        break;
                    case "email":
                        _type = InputTypeEnum.EMail;
                        break;
                    case "month":
                        _type = InputTypeEnum.Month;
                        break;
                    case "number":
                        _type = InputTypeEnum.Number;
                        break;
                    case "range":
                        _type = InputTypeEnum.Range;
                        break;
                    case "search":
                        _type = InputTypeEnum.Search;
                        break;
                    case "tel":
                        _type = InputTypeEnum.Tel;
                        break;
                    case "time":
                        _type = InputTypeEnum.Time;
                        break;
                    case "url":
                        _type = InputTypeEnum.URL;
                        break;
                    case "week":
                        _type = InputTypeEnum.Week;
                        break;
                    default:
                        _type = InputTypeEnum.Invalid;
                        AttributeHasValue = false;
                        break;
                }
            }
        }

        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, Value));            
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xObject = element.Attribute(AttributeName);
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }

        #endregion
    }
}
