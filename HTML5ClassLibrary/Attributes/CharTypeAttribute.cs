using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    public class CharAttribute : BaseAttribute
    {
        private Character _attrObject = new Character();

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(GetAttributeName(), _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(GetAttributeName());
            if ((xObject != null) && (xObject.Value.Length > 0))
            {
                _attrObject = new Character {Value = xObject.Value[0]};
                AttributeHasValue = true;
            }

        }

        public override string Value
        {
            get { return string.Format("{0}", _attrObject.Value); }
            set
            {
                if (value != string.Empty)
                {
                    _attrObject.Value = value[0];
                    AttributeHasValue = true;
                }
                else
                {
                    AttributeHasValue = false;
                }
            }
        }
    }
}
