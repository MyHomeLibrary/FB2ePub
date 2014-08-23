using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class VAlignTypeAttribute : BaseAttribute
    {
        private enum VAlign
        {
            Invalid,
            Top,
            Middle,
            Bottom,
            BaseLine,
        }

        public override string Value
        {
            get
            {
                switch (_valign)
                {
                    case VAlign.Middle:
                        return "middle";
                    case VAlign.BaseLine:
                        return "baseline";
                    case VAlign.Bottom:
                        return "bottom";
                    case VAlign.Top:
                        return "top";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "middle":
                        _valign = VAlign.Middle;
                        break;
                    case "baseline":
                        _valign = VAlign.BaseLine;
                        break;
                    case "bottom":
                        _valign = VAlign.Bottom;
                        break;
                    case "top":
                        _valign = VAlign.Top;
                        break;
                    default:
                        _valign = VAlign.Invalid;
                        break;
                }
            }
        }


        private VAlign _valign = VAlign.Invalid;

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
