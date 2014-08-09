using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISOLanguages;

namespace HTML5ClassLibrary.AttributeDataTypes
{
    /// <summary>
    /// A language code. For example, fr or en-gb
    /// </summary>
    public class LanguageCode
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
