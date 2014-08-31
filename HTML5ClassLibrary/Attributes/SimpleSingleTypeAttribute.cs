using System;
using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;


namespace XHTMLClassLibrary.Attributes
{
    public class SimpleSingleTypeAttribute<T> : BaseAttribute where T : IAttributeDataType, new()
    {
        protected T AttrObject = new T();

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(GetAttributeName(), AttrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            AttrObject = default(T);
            XAttribute xObject = element.Attribute(GetAttributeName());
            if (xObject != null)
            {
                AttrObject = new T {Value = xObject.Value};
                AttributeHasValue = true;
            }

        }

        public override object Value
        {
            get { return AttrObject.Value; }
            set
            {
                if (!(value is string) && !(value is T)) 
                    throw new ArgumentException(string.Format("The value set can be only of string or {0} type",typeof(T).Name));

                if (value is string)
                {
                    AttrObject.Value = value as string;
                    AttributeHasValue = (value as string != string.Empty);
                }
                else
                {
                    AttrObject = (T) value;
                    AttributeHasValue = AttrObject.Value != string.Empty;
                }
            }
        }
    }

}
