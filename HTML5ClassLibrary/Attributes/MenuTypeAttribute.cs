using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
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




        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(GetAttributeName(), Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xObject = element.Attribute(GetAttributeName());
            if (xObject != null)
            {
                Value = xObject.Value;
            }
        }
    }
}
