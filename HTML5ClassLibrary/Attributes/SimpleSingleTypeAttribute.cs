using System;
using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;
using XHTMLClassLibrary.BaseElements;


namespace XHTMLClassLibrary.Attributes
{
    public class SimpleSingleTypeAttribute<T> : BaseAttribute where T : IAttributeDataType, new()
    {
        protected T AttrObject = new T();


        public SimpleSingleTypeAttribute(string name)
            : base(name)
        {
        }

        public override void AddAttribute(XElement xElement, XNamespace ns)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(ns != null
                ? new XAttribute(ns + GetAttributeName(), AttrObject.Value)
                : new XAttribute(GetAttributeName(), AttrObject.Value));
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

                var str = value as string;
                if (str != null)
                {
                    AttrObject.Value = str;
                    AttributeHasValue =  true;                   
                }
                else
                {
                    AttrObject = (T) value;
                    AttributeHasValue = AttrObject.Value != null;
                }
            }
        }
    }

}
