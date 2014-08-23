using System.ComponentModel;
using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public enum TextDirection
    {
        Unknown,
        LtR, // left to right
        RtL, // Right to left
        Auto,
    }

    /// <summary>
    /// The dir attribute specifies the text direction of the element's content.
    /// </summary>
    public class DirectionTypeAttribute : BaseAttribute
    {
        private TextDirection _direction = TextDirection.Unknown;

        #region Implementation of IBaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            string dir;
            switch (_direction)
            {
                case TextDirection.LtR:
                    dir = "ltr";
                    break;
                case TextDirection.RtL:
                    dir = "rtl";
                    break;
                case TextDirection.Auto:
                    dir = "auto";
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
            var xLang = new XAttribute(GetAttributeName(), dir);
            xElement.Add(xLang);

        }

        public override void ReadAttribute(XElement element)
        {
            _direction = TextDirection.Unknown;
            AttributeHasValue = false;
            XAttribute xDirection = element.Attribute(GetAttributeName());
            if (xDirection != null)
            {
                AttributeHasValue = true;
                switch (xDirection.Value.ToLower())
                {
                    case "ltr":
                        _direction = TextDirection.LtR;
                        break;
                    case "rtl":
                        _direction = TextDirection.RtL;
                        break;
                    case "auto":
                        _direction = TextDirection.Auto;
                        break;
                    default:
                        _direction = TextDirection.Unknown;
                        break;
                }
            }

        }

        public override string Value
        {
            get
            {
                switch (_direction)
                {
                    case TextDirection.LtR:
                        return "ltr";
                    case TextDirection.RtL:
                        return "rtl";
                }
                return string.Empty;
            }
            set
            {
                AttributeHasValue = true;
                switch (value.ToLower())
                {
                    case "ltr":
                        _direction = TextDirection.LtR;
                        break;
                    case "rtl":
                        _direction = TextDirection.RtL;
                        break;
                    default:
                        _direction = TextDirection.Unknown;
                        AttributeHasValue = false;
                        break;
                }
                
            }
        }

        #endregion
    }
}
