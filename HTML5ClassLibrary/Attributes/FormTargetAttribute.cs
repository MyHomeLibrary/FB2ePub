using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes
{
    public class FormTargetAttribute: BaseAttribute
    {
        private URI _frameNameURI = new URI();

        private enum TargetType
        {
            Invalid,
            Blank,
            Self,
            Parent,
            Top,
            FrameName
        }

        public override string Value
        {
            get
            {
                switch (_target)
                {
                    case TargetType.Blank:
                        return "_blank";
                    case TargetType.Self:
                        return "_self";
                    case TargetType.Parent:
                        return "_parent";
                    case TargetType.Top:
                        return "_top";
                    case TargetType.FrameName:
                        return _frameNameURI.Value;
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "_blank":
                        _target = TargetType.Blank;
                        break;
                    case "_self":
                        _target = TargetType.Self;
                        break;
                    case "_parent":
                        _target = TargetType.Parent;
                        break;
                    case "_top":
                        _target = TargetType.Top;
                        break;
                    case null:
                    case "":
                        _target = TargetType.Invalid;
                        break;
                    default:
                        _target = TargetType.FrameName;
                        _frameNameURI.Value = value;
                        break;
                }
            }
        }


        private TargetType _target = TargetType.Invalid;

        private const string AttributeName = "formtarget";

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
