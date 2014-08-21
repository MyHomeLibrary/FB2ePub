using System.Xml.Linq;
using ISOLanguages;

namespace XHTMLClassLibrary.Attributes
{
    /// <summary>
    /// The lang attribute specifies the language of the element's content.
    /// </summary>
    public class LanguagesTypeAttribute : BaseAttribute
    {
        private Languages  _language = new Languages();

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(GetAttributeName(), _language.GetAsIsoName()));

        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xLanguage = element.Attribute(GetAttributeName());
            if (xLanguage != null)
            {
                _language = new Languages();
                _language.SetLanguageId(xLanguage.Value);
                AttributeHasValue = true;
            }

        }
        public override string Value
        {
            get { return _language.GetAsIsoName(); }
            set
            {
                _language.SetLanguageId(value);
                AttributeHasValue = (_language.Language != LanguagesEnum.Unknown);
            }
        }
    }
}
