using System.Xml.Serialization;

namespace TranslitRuContracts
{
    public enum TranslitModeEnum
    {
        None,
        TranslitRu, // use mode used by http://www.translit.ru
        ExternalRuleFile, // use external maping file
        HtmlCodes, // HTML codes
    }

    public interface ITransliterationSettings : IXmlSerializable
    {
        TranslitModeEnum Mode { get; }
        string FileName { get;  }
        bool Transliterate { get; }

        void CopyFrom(ITransliterationSettings temp);
        void SetupDefaults();
    }
}
