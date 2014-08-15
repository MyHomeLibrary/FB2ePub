using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HTMLClassLibrary.Attributes
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
                AttributeHasValue = true;
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
                        AttributeHasValue = false;
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
