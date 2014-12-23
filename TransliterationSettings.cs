namespace TranslitRu
{
    public enum TranslitModeEnum
    {
        None,
        TranslitRu, // use mode used by http://www.translit.ru
        ExternalRuleFile, // use external maping file
        HtmlCodes, // HTML codes
    }

    public class TransliterationSettings
    {
        public TranslitModeEnum Mode { get; set; }
        public string FileName { get; set; }
    }
}
