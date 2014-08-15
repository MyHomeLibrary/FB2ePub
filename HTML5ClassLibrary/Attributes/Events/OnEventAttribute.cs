using System.Xml.Linq;
using HTMLClassLibrary.AttributeDataTypes;

namespace HTMLClassLibrary.Attributes.Events
{
    /// <summary>
    /// Defines base class for event attribute
    /// </summary>
    public abstract class OnEventAttribute : BaseAttribute
    {
        private Script _attrObject = new Script();

        protected abstract string GetAttributeName();

        #region Overrides of BaseAttribute

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
                _attrObject = new Script {Value = xObject.Value};
                AttributeHasValue = true;
            }
        }

        public override string Value
        {
            get
            {
                return _attrObject.Value;
            }
            set
            {
                _attrObject.Value = value;
                AttributeHasValue = true;
            }
        }

        #endregion
    }
}