using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class MethodAttribute : BaseAttribute
    {
        private enum MethodEnum
        {
            Invalid,
            Get,
            Post,
        }

        public override string Value
        {
            get
            {
                switch (_method)
                {
                    case MethodEnum.Post:
                        return "post";
                    case MethodEnum.Get:
                        return "get";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "post":
                        _method = MethodEnum.Post;
                        break;
                    case "get":
                        _method = MethodEnum.Get;
                        break;
                    default:
                        _method = MethodEnum.Invalid;
                        break;
                }
            }
        }


        private MethodEnum _method = MethodEnum.Invalid;

        private const string AttributeName = "method";

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
