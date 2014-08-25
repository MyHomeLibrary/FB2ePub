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

        public override string Value
        {
            get { return AttrObject.Value; }
            set
            {
                AttrObject.Value = value;
                AttributeHasValue = (value != string.Empty);
            }
        }
    }

}
