using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTML5ClassLibrary.Attributes
{
    public class SandboxAttribute : BaseAttribute
    {
        private enum Restrictions
        {
            Invalid,
            AllRestrictions,
            AllowForms,
            AllowSameOrigin,
            AllowScripts,
            AllowTopNavigation,
        }

        private Restrictions _restriction = Restrictions.Invalid;

        private const string AttributeName = "sandbox";

        #region Overrides of BaseAttribute

        public override string Value
        {
            get
            {
                switch (_restriction)
                {
                    case Restrictions.AllowForms:
                        return "allow-forms";
                    case Restrictions.AllowSameOrigin:
                        return "allow-same-origins";
                    case Restrictions.AllowScripts:
                        return "allow-scripts";
                    case Restrictions.AllowTopNavigation:
                        return "allow-top-navigation";
                    case Restrictions.AllRestrictions:
                        return "";
                }
                return string.Empty;
            }

            set
            {
                _hasValue = true;
                switch (value.ToLower())
                {
                    case "allow-forms":
                        _restriction = Restrictions.AllowForms;
                        break;
                    case "allow-same-origin":
                        _restriction = Restrictions.AllowSameOrigin;
                        break;
                    case "allow-scripts":
                        _restriction = Restrictions.AllowScripts;
                        break;
                    case "allow-top-navigation":
                        _restriction = Restrictions.AllowTopNavigation;
                        break;
                    case "":
                        _restriction = Restrictions.AllRestrictions;
                        break;
                    default:
                        _restriction = Restrictions.Invalid;
                        _hasValue = false;
                        break;
                }
            }
        }



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
