using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOLanguages
{
    public enum LanguagesEnum
    {
        Unknown,
        Afrikaans,
        Arabic,
        Albanian,
        Armenian,
        Bashkir,
        Bengali,
        Bosnian,
        Breton,
        Bulgarian,
        Burmese,
        Catalan,
        Chinese,
        Croatian,
        Czech,
        Danish,
        Dutch,
        EnglishAustralian,
        EnglishBritish,
        EnglishCanadian,
        EnglishUS,
        Esperanto,
        Estonian,
        Finnish,
        French,
        FrenchCanadian,
        Gaelic,
        Georgian,
        German,
        GermanSwiss,
        GermanAustrian,
        Greek,
        Gujurati,
        Hebrew,
        Hindi,
        Hungarian,
        Icelandic,
        Indonesian,
        Iranian,
        Irish,
        Italian,
        Japanese,
        Javanese,
        Korean,
        Latin,
        Latvian,
        Lithuanian,
        Luxenbourgish,
        Macedonian,
        Malayalam,
        Maltese,
        Moldovian,
        Norwegian,
        Panjabi,
        Persian,
        Polish,
        Portuguese,
        Romanian,
        Russian,
        Serbian,
        Slovak,
        Slovenian,
        Spanish,
        Sudanese,
        Swahili,
        Swedish,
        Thai,
        Turkish,
        Ukrainian,
        Urdu,
        Vietnamese,
        Walloon,
        Welsh,
        Zulu,
        Other, // other , unknown language
    }

    public class Languages
    {
        private LanguagesEnum lang = LanguagesEnum.Unknown;
        private string otherLangISO = string.Empty;

        public LanguagesEnum Language
        {
            get { return lang; } 
            set
            {
                lang = value;
                otherLangISO = string.Empty;
            }
        }

        public void SetLanguageId (string otherLanguge)
        {
            switch (otherLanguge.ToLower())
            {
                case "af":
                    lang = LanguagesEnum.Afrikaans;
                    break;
                case "ar":
                    lang = LanguagesEnum.Arabic;
                    break;
                case "sq":
                    lang = LanguagesEnum.Albanian;
                    break;
                case "hy":
                    lang = LanguagesEnum.Armenian;
                    break;
                case "ba":
                    lang = LanguagesEnum.Bashkir;
                    break;
                case "bn":
                    lang = LanguagesEnum.Bengali;
                    break;
                case "bs":
                    lang = LanguagesEnum.Bosnian;
                    break;
                case "br":
                    lang = LanguagesEnum.Breton;
                    break;
                case "bg":
                    lang = LanguagesEnum.Bulgarian;
                    break;
                case "my":
                    lang = LanguagesEnum.Burmese;
                    break;
                case "ca":
                    lang = LanguagesEnum.Catalan;
                    break;
                case "zh":
                    lang = LanguagesEnum.Chinese;
                    break;
                case "hr":
                    lang = LanguagesEnum.Croatian;
                    break;
                case "cs":
                    lang = LanguagesEnum.Czech;
                    break;
                case "da":
                    lang = LanguagesEnum.Danish;
                    break;
                case "nl":
                    lang = LanguagesEnum.Dutch;
                    break;
                case "en-au":
                    lang = LanguagesEnum.EnglishAustralian;
                    break;
                case "en":
                    lang = LanguagesEnum.EnglishBritish;
                    break;
                case "en-ca":
                    lang = LanguagesEnum.EnglishCanadian;
                    break;
                case "en-us":
                    lang = LanguagesEnum.EnglishUS;
                    break;
                case "eo":
                    lang = LanguagesEnum.Esperanto;
                    break;
                case "et":
                    lang = LanguagesEnum.Estonian;
                    break;
                case "fi":
                    lang = LanguagesEnum.Finnish;
                    break;
                case "fr":
                    lang = LanguagesEnum.French;
                    break;
                case "fr-ca":
                    lang = LanguagesEnum.FrenchCanadian;
                    break;
                case "gd":
                    lang = LanguagesEnum.Gaelic;
                    break;
                case "ka":
                    lang = LanguagesEnum.Georgian;
                    break;
                case "de":
                    lang = LanguagesEnum.German;
                    break;
                case "de-ch":
                    lang = LanguagesEnum.GermanSwiss;
                    break;
                case "de-at":
                    lang = LanguagesEnum.GermanAustrian;
                    break;
                case "el":
                    lang = LanguagesEnum.Greek;
                    break;
                case "gu":
                    lang = LanguagesEnum.Gujurati;
                    break;
                case "he":
                    lang = LanguagesEnum.Hebrew;
                    break;
                case "hi":
                    lang = LanguagesEnum.Hindi;
                    break;
                case "hu":
                    lang = LanguagesEnum.Hungarian;
                    break;
                case "is":
                    lang = LanguagesEnum.Icelandic;
                    break;
                case "id":
                    lang = LanguagesEnum.Indonesian;
                    break;
                case "ira":
                    lang = LanguagesEnum.Iranian;
                    break;
                case "ga":
                    lang = LanguagesEnum.Irish;
                    break;
                case "it":
                    lang = LanguagesEnum.Italian;
                    break;
                case "jp":
                case "ja":
                    lang = LanguagesEnum.Japanese;
                    break;
                case "jv":
                    lang = LanguagesEnum.Javanese;
                    break;
                case "ko":
                    lang = LanguagesEnum.Korean;
                    break;
                case "la":
                    lang = LanguagesEnum.Latin;
                    break;
                case "lv":
                    lang = LanguagesEnum.Latvian;
                    break;
                case "lt":
                    lang = LanguagesEnum.Lithuanian;
                    break;
                case "lb":
                    lang = LanguagesEnum.Luxenbourgish;
                    break;
                case "mk":
                    lang = LanguagesEnum.Macedonian;
                    break;
                case "ml":
                    lang = LanguagesEnum.Malayalam;
                    break;
                case "mt":
                    lang = LanguagesEnum.Maltese;
                    break;
                case "mo":
                    lang = LanguagesEnum.Moldovian;
                    break;
                case "no":
                    lang = LanguagesEnum.Norwegian;
                    break;
                case "pa":
                    lang = LanguagesEnum.Panjabi;
                    break;
                case "fa":
                    lang = LanguagesEnum.Persian;
                    break;
                case "pl":
                    lang = LanguagesEnum.Polish;
                    break;
                case "pt":
                    lang =  LanguagesEnum.Portuguese;
                    break;
                case "ro":
                    lang = LanguagesEnum.Romanian;
                    break;
                case "ru":
                    lang = LanguagesEnum.Russian;
                    break;
                case "sr":
                    lang = LanguagesEnum.Serbian;
                    break;
                case "sk":
                    lang = LanguagesEnum.Slovak;
                    break;
                case "sl":
                    lang = LanguagesEnum.Slovenian;
                    break;
                case "es":
                    lang = LanguagesEnum.Spanish;
                    break;
                case "su":
                    lang = LanguagesEnum.Sudanese;
                    break;
                case "sw":
                    lang = LanguagesEnum.Swahili;
                    break;
                case "sv":
                    lang = LanguagesEnum.Swedish;
                    break;
                case "th":
                    lang = LanguagesEnum.Thai;
                    break;
                case "tr":
                    lang = LanguagesEnum.Turkish;
                    break;
                case "uk":
                    lang = LanguagesEnum.Ukrainian;
                    break;
                case "ur":
                    lang = LanguagesEnum.Urdu;
                    break;
                case "vi":
                    lang = LanguagesEnum.Vietnamese;
                    break;
                case "wa":
                    lang = LanguagesEnum.Walloon;
                    break;
                case "cy":
                    lang = LanguagesEnum.Welsh;
                    break;
                case "zu":
                    lang = LanguagesEnum.Zulu;
                    break;
                case "":
                    lang = LanguagesEnum.Unknown;
                    break;
                default:
                    otherLangISO = otherLanguge;
                    lang = LanguagesEnum.Other;
                    break;
            }

            //otherLangISO = otherLanguge;
            //lang = LanguagesEnum.Other;
        }

        // return RFC 3066 format language name 
        public string GetAsIsoName()
        {
            switch (Language)
            {
                case LanguagesEnum.Unknown:
                    return string.Empty;
                case LanguagesEnum.Afrikaans:
                    return "af";
                case LanguagesEnum.Arabic:
                    return "ar";
                case LanguagesEnum.Albanian:
                    return "sq";
                case LanguagesEnum.Armenian:
                    return "hy";
                case LanguagesEnum.Bashkir:
                    return "ba";
                case LanguagesEnum.Bengali:
                    return "bn";
                case LanguagesEnum.Bosnian:
                    return "bs";
                case LanguagesEnum.Breton:
                    return "br";
                case LanguagesEnum.Bulgarian:
                    return "bg";
                case LanguagesEnum.Burmese:
                    return "my";
                case LanguagesEnum.Catalan:
                    return "ca";
                case LanguagesEnum.Chinese:
                    return "zh";
                case LanguagesEnum.Croatian:
                    return "hr";
                case LanguagesEnum.Czech:
                    return "cs";
                case LanguagesEnum.Danish:
                    return "da";
                case LanguagesEnum.Dutch:
                    return "nl";
                case LanguagesEnum.EnglishAustralian:
                    return "en-au";
                case LanguagesEnum.EnglishBritish:
                    return "en";
                case LanguagesEnum.EnglishCanadian:
                    return "en-ca";
                case LanguagesEnum.EnglishUS:
                    return "en-us";
                case LanguagesEnum.Esperanto:
                    return "eo";
                case LanguagesEnum.Estonian:
                    return "et";
                case LanguagesEnum.Finnish:
                    return "fi";
                case LanguagesEnum.French:
                    return "fr";
                case LanguagesEnum.FrenchCanadian:
                    return "fr-ca";
                case LanguagesEnum.Gaelic:
                    return "gd";
                case LanguagesEnum.Georgian:
                    return "ka";
                case LanguagesEnum.German:
                    return "de";
                case LanguagesEnum.GermanSwiss:
                    return "de-ch";
                case LanguagesEnum.GermanAustrian:
                    return "de-at";
                case LanguagesEnum.Greek:
                    return "el";
                case LanguagesEnum.Gujurati:
                    return "gu";
                case LanguagesEnum.Hebrew:
                    return "he";
                case LanguagesEnum.Hindi:
                    return "hi";
                case LanguagesEnum.Hungarian:
                    return "hu";
                case LanguagesEnum.Icelandic:
                    return "is";
                case LanguagesEnum.Indonesian:
                    return "id";
                case LanguagesEnum.Iranian:
                    return "ira";
                case LanguagesEnum.Irish:
                    return "ga";
                case LanguagesEnum.Italian:
                    return "it";
                case LanguagesEnum.Japanese:
                    return "ja";
                case LanguagesEnum.Javanese:
                    return "jv";
                case LanguagesEnum.Korean:
                    return "ko";
                case LanguagesEnum.Latin:
                    return "la";
                case LanguagesEnum.Latvian:
                    return "lv";
                case LanguagesEnum.Lithuanian:
                    return "lt";
                case LanguagesEnum.Luxenbourgish:
                    return "lb";
                case LanguagesEnum.Macedonian:
                    return "mk";
                case LanguagesEnum.Malayalam:
                    return "ml";
                case LanguagesEnum.Maltese:
                    return "mt";
                case LanguagesEnum.Moldovian:
                    return "mo";
                case LanguagesEnum.Norwegian:
                    return "no";
                case LanguagesEnum.Panjabi:
                    return "pa";
                case LanguagesEnum.Persian:
                    return "fa";
                case LanguagesEnum.Polish:
                    return "pl";
                case LanguagesEnum.Portuguese:
                    return "pt";
                case LanguagesEnum.Romanian:
                    return "ro";
                case LanguagesEnum.Russian:
                    return "ru";
                case LanguagesEnum.Serbian:
                    return "sr";
                case LanguagesEnum.Slovak:
                    return "sk";
                case LanguagesEnum.Slovenian:
                    return "sl";
                case LanguagesEnum.Spanish:
                    return "es";
                case LanguagesEnum.Sudanese:
                    return "su";
                case LanguagesEnum.Swahili:
                    return "sw";
                case LanguagesEnum.Swedish:
                    return "sv";
                case LanguagesEnum.Thai:
                    return "th";
                case LanguagesEnum.Turkish:
                    return "tr";
                case LanguagesEnum.Ukrainian:
                    return "uk";
                case LanguagesEnum.Urdu:
                    return "ur";
                case LanguagesEnum.Vietnamese:
                    return "vi";
                case LanguagesEnum.Walloon:
                    return "wa";
                case LanguagesEnum.Welsh:
                    return "cy";
                case LanguagesEnum.Zulu:
                    return "zu";
                case LanguagesEnum.Other: // other , unknown language
                    return otherLangISO;
            }
            return "";
        }
    }
}