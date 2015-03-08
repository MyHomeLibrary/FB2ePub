using TranslitRuContracts;

namespace TranslitRu
{


    public class TransliterationSettings : ITransliterationSettings
    {
        public TranslitModeEnum Mode { get; set; }
        public string FileName { get; set; }
    }
}
