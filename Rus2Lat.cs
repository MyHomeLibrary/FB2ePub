using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TranslitRu
{
    public enum TranslitModeEnum
    {
        None,
        TranslitRu, // use mode used by http://www.translit.ru
        ExternalRuleFile, // use external maping file
        HtmlCodes, // HTML codes
    }

    public class Rus2Lat
    {
        public static Rus2Lat Instance;

        static Rus2Lat()
        {
            Instance = new Rus2Lat();
        }

        private Rus2Lat()
        {
            
        }

        /// <summary>
        /// Name of the rule file used
        /// </summary>
        public string RuleFile { get; set; }

        public string Translate(string stringWithCyrillic, TranslitModeEnum mode)
        {
            ITranslitConverter converter;
            switch (mode)
            {
                case TranslitModeEnum.None:
                    return stringWithCyrillic;
                case TranslitModeEnum.TranslitRu:
                    converter = new TransLitRuConverter();
                    return converter.ConvertString(stringWithCyrillic);
                case TranslitModeEnum.ExternalRuleFile:
                    converter = new FileBasedConverter { FileName = RuleFile };
                    return converter.ConvertString(stringWithCyrillic);
                case TranslitModeEnum.HtmlCodes:
                    converter = new HtmlConverter();
                    return converter.ConvertString(stringWithCyrillic);
                default:
                    Debug.Fail(string.Format("Invalid transliteration mode : {0}",mode));
                    break;
            }
            return stringWithCyrillic;
        }
    }
}
