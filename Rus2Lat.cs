using System;
using System.Collections.Generic;
using System.Diagnostics;
using TranslitRuContracts;

namespace TranslitRu
{

    public class Rus2Lat
    {
        public static readonly Rus2Lat Instance = new Rus2Lat();
        private readonly Dictionary<string,ITranslitConverter>  _fileConverters = new Dictionary<string, ITranslitConverter>();


        private Rus2Lat()
        {
            
        }

        public string Translate(string stringWithCyrillic, ITransliterationSettings settings)
        {
            ITranslitConverter converter;
            switch (settings.Mode)
            {
                case TranslitModeEnum.None:
                    return stringWithCyrillic;
                case TranslitModeEnum.TranslitRu:
                    converter = new TransLitRuConverter();
                    return converter.ConvertString(stringWithCyrillic);
                case TranslitModeEnum.ExternalRuleFile:
                    if (settings.FileName == null)
                    {
                        throw new ArgumentNullException("settings", "When transliterating using external file , file name need to be provided");
                    }
                    if (_fileConverters.ContainsKey(settings.FileName))
                    {
                        converter = _fileConverters[settings.FileName];
                    }
                    else
                    {
                        converter = new FileBasedConverter { FileName = settings.FileName };
                        _fileConverters[settings.FileName] = converter;
                    }
                    return converter.ConvertString(stringWithCyrillic);
                case TranslitModeEnum.HtmlCodes:
                    converter = new HtmlConverter();
                    return converter.ConvertString(stringWithCyrillic);
                default:
                    Debug.Fail(string.Format("Invalid transliteration mode : {0}", settings.Mode));
                    break;
            }
            return stringWithCyrillic;
        }
    }
}
