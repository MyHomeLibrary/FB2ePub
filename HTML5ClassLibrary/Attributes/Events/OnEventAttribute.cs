using System.Xml.Linq;
using HTML5ClassLibrary.AttributeDataTypes;

namespace HTML5ClassLibrary.Attributes.Events
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
            if (!_hasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(GetAttributeName(), _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            _hasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(GetAttributeName());
            if (xObject != null)
            {
                _attrObject = new Script {Value = xObject.Value};
                _hasValue = true;
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
                _hasValue = true;
            }
        }

        #endregion
    }
}