using ISOLanguages;

namespace XHTMLClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// A language code. For example, fr or en-gb
    /// </summary>
    public class LanguageCode : IAttributeDataType
    {
        private readonly Languages _language = new Languages();

        public string Value
        {
            get
            {
                return _language.GetAsIsoName();
            }

            set
            {
                _language.SetLanguageId(value);
            }
        }
    }
}
