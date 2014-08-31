using System;
using System.Xml.Linq;

namespace XHTMLClassLibrary.Attributes
{
    public class FlagTypeAttribute : BaseAttribute
    {
        private readonly bool _putValue;


        public FlagTypeAttribute(bool putNameAsValue)
        {
            _putValue = putNameAsValue;
        }

        public FlagTypeAttribute()
        {
            _putValue = false;
        }

        public override void AddAttribute(XElement xElement, XNamespace ns)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(ns + GetAttributeName(), _putValue?GetAttributeName():string.Empty));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xObject = element.Attribute(GetAttributeName());
            if (xObject != null)
            {
                AttributeHasValue = true;
            }
        }

        public override object Value
        {
            get
            {
                return GetAttributeName();
            }
            set
            {
                AttributeHasValue = false;
                if ( !(value is bool) && !(value is string))
                    return;
                if (value is bool)
                {
                    AttributeHasValue = (bool)value;
                    return;
                }
                if (string.Compare((value as string), GetAttributeName(), StringComparison.Ordinal) == 0)
                {
                    AttributeHasValue = true;
                }
                else
                {
                    bool hasValue;
                    if (bool.TryParse(value as string, out hasValue))
                    {
                        AttributeHasValue = hasValue;
                    }
                }
            }
        }
    }
}