using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class WrapTypeAttribute : BaseAttribute
    {
        private enum WrapValues
        {
            Invalid,
            Hard,
            Soft,
        }

        private WrapValues _wrap = WrapValues.Invalid;

        public override string Value
        {
            get
            {
                switch (_wrap)
                {
                    case WrapValues.Hard:
                        return "hard";
                    case WrapValues.Soft:
                        return "soft";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "hard":
                        _wrap = WrapValues.Hard;
                        break;
                    case "soft":
                        _wrap = WrapValues.Soft;
                        break;
                    default:
                        _wrap = WrapValues.Invalid;
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
