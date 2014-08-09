using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class FrameAttribute : BaseAttribute
    {
        private enum FrameTypeEnum
        {
            Invalid,
            Void,
            Above,
            Below,
            HSides,
            VSides,
            LeftHandSide,
            RightHandSide,
            Box,
            Border,
        }

        public override string Value
        {
            get
            {
                switch (_type)
                {
                    case FrameTypeEnum.Above:
                        return "above";
                    case FrameTypeEnum.HSides:
                        return "hsides";
                    case FrameTypeEnum.Below:
                        return "below";
                    case FrameTypeEnum.Void:
                        return "void";
                    case FrameTypeEnum.VSides:
                        return "vsides";
                    case FrameTypeEnum.LeftHandSide:
                        return "lhs";
                    case FrameTypeEnum.RightHandSide:
                        return "rhs";
                    case FrameTypeEnum.Box:
                        return "box";
                    case FrameTypeEnum.Border:
                        return "border";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "above":
                        _type = FrameTypeEnum.Above;
                        break;
                    case "hsides":
                        _type = FrameTypeEnum.HSides;
                        break;
                    case "below":
                        _type = FrameTypeEnum.Below;
                        break;
                    case "void":
                        _type = FrameTypeEnum.Void;
                        break;
                    case "vsides":
                        _type = FrameTypeEnum.VSides;
                        break;
                    case "lhs":
                        _type = FrameTypeEnum.LeftHandSide;
                        break;
                    case "rhs":
                        _type = FrameTypeEnum.RightHandSide;
                        break;
                    case "box":
                        _type = FrameTypeEnum.Box;
                        break;
                    case "border":
                        _type = FrameTypeEnum.Border;
                        break;
                    default:
                        _type = FrameTypeEnum.Invalid;
                        break;
                }
            }
        }


        private FrameTypeEnum _type = FrameTypeEnum.Invalid;

        private const string AttributeName = "frame";

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
