using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class BooleanSimpleTypeAttribute : BaseAttribute
    {
        private enum States
        {
            Invalid,
            True,
            False,
        }

        private States _state = States.Invalid;


        public override string Value
        {
            get
            {
                switch (_state)
                {
                    case States.True:
                        return "true";
                    case States.False:
                        return "false";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "true":
                        _state = States.True;
                        break;
                    case "false":
                        _state = States.False;
                        break;
                    default:
                        _state = States.Invalid;
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
