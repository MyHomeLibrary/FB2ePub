using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HTMLClassLibrary.AttributeDataTypes;

namespace HTMLClassLibrary.Attributes
{
    public class PatternAttribute : BaseAttribute
    {
        private PatternAttributeType _attrObject = new PatternAttributeType();

        private const string AttributeName = "pattern";

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
            if (xObject != null)
            {
                _attrObject = new PatternAttributeType { Value = xObject.Value };
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
        #endregion

    }
}
