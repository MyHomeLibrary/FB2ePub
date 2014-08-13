using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes
{
    public class CharAttribute : BaseAttribute
    {
        private Character _attrObject = new Character();

        private const string AttributeName = "char";

        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(AttributeName, _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(AttributeName);
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
        #endregion

    }
}
