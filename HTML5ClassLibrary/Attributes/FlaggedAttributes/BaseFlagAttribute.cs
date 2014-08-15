using System.Xml.Linq;
using HTMLClassLibrary.AttributeDataTypes;

namespace HTMLClassLibrary.Attributes.FlaggedAttributes
{
    public abstract class BaseFlagAttribute : BaseAttribute
    {
        private Text _attrObject = new Text();

        public abstract string GetElementName();

        #region Overrides of BaseAttribute

        /// <summary>
        /// Set the attribute to on/off (disabled or not)
        /// </summary>
        /// <param name="flag">on/off Boolean flag</param>
        public void SetFlag(bool flag)
        {
            if (flag)
            {
                _attrObject.Value = GetElementName();
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
            xElement.Add(new XAttribute(GetElementName(), _attrObject.Value));
        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            _attrObject = null;
            XAttribute xObject = element.Attribute(GetElementName());
            if (xObject != null)
            {
                _attrObject = new Text();
                _attrObject.Value = xObject.Value;
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
                AttributeHasValue = (value != string.Empty);
            }
        }

        #endregion

    }
}