using System.Xml.Linq;
using XHTMLClassLibrary.AttributeDataTypes;

namespace XHTMLClassLibrary.Attributes
{
    public class ISMapTypeAttribute : BaseAttribute
    {
        private Text _attrObject = new Text();

        /// <summary>
        /// Set the attribute to on/off (ismap or not)
        /// </summary>
        /// <param name="disabled"></param>
        public void SetISMap(bool disabled)
        {
            if (disabled)
            {
                _attrObject.Value = "ismap";
                AttributeHasValue = true;
            }
            else
            {
                _attrObject.Value = string.Empty;
                AttributeHasValue = false;
            }
        }

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
            if (xObject != null)
            {
                _attrObject = new Text {Value = xObject.Value};
                AttributeHasValue = true;
            }
        }

        public override string Value
        {
            get { return _attrObject.Value; }
            set
            {
                _attrObject.Value = value;
                AttributeHasValue = (value != string.Empty);
            }
        }
    }
}
