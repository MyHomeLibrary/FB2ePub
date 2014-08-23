using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class FormMethodTypeAttribute: BaseAttribute
    {
        private enum MethodTypes
        {
            Invalid,
            Get,
            Post

        }

        public override string Value
        {
            get
            {
                switch (_methodType)
                {
                    case MethodTypes.Get:
                        return "get";
                    case MethodTypes.Post:
                        return "post";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "get":
                        _methodType = MethodTypes.Get;
                        break;
                    case "post":
                        _methodType = MethodTypes.Post;
                        break;
                    default:
                        _methodType = MethodTypes.Invalid;
                        break;
                }
            }
        }


        private MethodTypes _methodType = MethodTypes.Invalid;


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
