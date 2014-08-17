using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class BiDirectionalAlignAttribute : BaseAttribute
    {
        private enum Align
        {
            Invalid,
            Left,
            Right,
            Top,
            Bottom,
            Middle,
            BaseLine,
        }
        private Align _align = Align.Invalid;

        private const string AttributeName = "align";


        public override string Value
        {
            get
            {
                switch (_align)
                {
                    case Align.Middle:
                        return "middle";
                    case Align.BaseLine:
                        return "baseline";
                    case Align.Bottom:
                        return "bottom";
                    case Align.Top:
                        return "top";
                    case Align.Left:
                        return "left";
                    case Align.Right:
                        return "right";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "middle":
                        _align = Align.Middle;
                        break;
                    case "baseline":
                        _align = Align.BaseLine;
                        break;
                    case "bottom":
                        _align = Align.Bottom;
                        break;
                    case "top":
                        _align = Align.Top;
                        break;
                    case "left":
                        _align = Align.Left;
                        break;
                    case "right":
                        _align = Align.Right;
                        break;
                    default:
                        _align = Align.Invalid;
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
