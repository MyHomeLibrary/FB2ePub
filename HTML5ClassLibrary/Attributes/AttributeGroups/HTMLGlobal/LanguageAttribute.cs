using System.Xml.Linq;
using ISOLanguages;

namespace HTML5ClassLibrary.Attributes.AttributeGroups.HTMLGlobal
{
    /// <summary>
    /// The lang attribute specifies the language of the element's content.
    /// </summary>
    public class LanguageAttribute : BaseAttribute
    {
        private Languages  _language = new Languages();
        private const string AttributeName = "lang";

        #region Overrides of BaseAttribute

        public override void AddAttribute(XElement xElement)
        {
            if (!AttributeHasValue)
            {
                return;
            }
            xElement.Add(new XAttribute(XNamespace.Xml + AttributeName, _language.GetAsIsoName()));

        }

        public override void ReadAttribute(XElement element)
        {
            AttributeHasValue = false;
            XAttribute xLanguage = element.Attribute(XNamespace.Xml + AttributeName);
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

        #endregion
    }
}
