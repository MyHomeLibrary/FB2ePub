using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HTMLClassLibrary.AttributeDataTypes;

namespace HTMLClassLibrary.Attributes
{
    /// <summary>
    /// The step attribute specifies the legal number intervals for an "input" element.
    /// Example: if step="3", legal numbers could be -3, 0, 3, 6, etc.
    /// Tip: The step attribute can be used together with the max and min attributes to create a range of legal values.
    /// Note: The step attribute works with the following input types: number, range, date, datetime, datetime-local, month, time and week.
    /// </summary>
    public class StepAttribute : BaseAttribute
    {
        private Text _attrObject = new Text();

        private const string AttributeName = "step";

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
                _attrObject = new Text { Value = xObject.Value };
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
