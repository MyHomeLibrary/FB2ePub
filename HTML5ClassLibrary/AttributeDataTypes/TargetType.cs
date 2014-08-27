namespace XHTMLClassLibrary.AttributeDataTypes
{
    public class TargetType : IAttributeDataType
    {
        private enum TargetTypeOptions
        {
            Invalid,
            Blank,
            Self,
            Parent,
            Top,
            FrameName
        }

        private readonly URI _frameNameURI = new URI();
        private TargetTypeOptions _target = TargetTypeOptions.Invalid;

        public string Value
        {
            get
            {
                switch (_target)
                {
                    case TargetTypeOptions.Blank:
                        return "_blank";
                    case TargetTypeOptions.Self:
                        return "_self";
                    case TargetTypeOptions.Parent:
                        return "_parent";
                    case TargetTypeOptions.Top:
                        return "_top";
                    case TargetTypeOptions.FrameName:
                        return _frameNameURI.Value;
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "_blank":
                        _target = TargetTypeOptions.Blank;
                        break;
                    case "_self":
                        _target = TargetTypeOptions.Self;
                        break;
                    case "_parent":
                        _target = TargetTypeOptions.Parent;
                        break;
                    case "_top":
                        _target = TargetTypeOptions.Top;
                        break;
                    case null:
                    case "":
                        _target = TargetTypeOptions.Invalid;
                        break;
                    default:
                        _target = TargetTypeOptions.FrameName;
                        _frameNameURI.Value = value;
                        break;
                }
            }
        }
    }
}
