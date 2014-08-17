using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class AutocompleteAttribute : BaseAttribute
    {
        private enum AutocompleteValues
        {
            Invalid,
            On,
            Off,
        }

        private AutocompleteValues _autocomplete = AutocompleteValues.Invalid;

        private const string AttributeName = "autocomplete";

        #region Overrides of BaseAttribute


        public override string Value
        {
            get
            {
                switch (_autocomplete)
                {
                    case AutocompleteValues.On:
                        return "on";
                    case AutocompleteValues.Off:
                        return "off";
                }
                return string.Empty;
            }

            set
            {
                switch (value.ToLower())
                {
                    case "on":
                        _autocomplete = AutocompleteValues.On;
                        break;
                    case "off":
                        _autocomplete = AutocompleteValues.Off;
                        break;
                    default:
                        _autocomplete = AutocompleteValues.Invalid;
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
