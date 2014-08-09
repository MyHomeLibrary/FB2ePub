using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class RulesAttribute : BaseAttribute
    {
        private enum RulesEnum
        {
            Invalid,
            None,
            Groups,
            Rows,
            Cols,
            All,
        }

        public override string Value
        {
            get
            {
                switch (_rules)
                {
                    case RulesEnum.Groups:
                        return "groups";
                    case RulesEnum.Cols:
                        return "cols";
                    case RulesEnum.Rows:
                        return "rows";
                    case RulesEnum.None:
                        return "none";
                    case RulesEnum.All:
                        return "all";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "groups":
                        _rules = RulesEnum.Groups;
                        break;
                    case "cols":
                        _rules = RulesEnum.Cols;
                        break;
                    case "rows":
                        _rules = RulesEnum.Rows;
                        break;
                    case "none":
                        _rules = RulesEnum.None;
                        break;
                    case "all":
                        _rules = RulesEnum.All;
                        break;
                    default:
                        _rules = RulesEnum.Invalid;
                        break;
                }
            }
        }


        private RulesEnum _rules = RulesEnum.Invalid;

        private const string AttributeName = "rules";

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
