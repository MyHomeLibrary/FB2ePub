using System.Xml.Serialization;
using FontSettingsContracts;
using TranslitRuContracts;

namespace ConverterContracts.Settings
{
    public enum IgnoreInfoSourceOptions
    {
        IgnoreNothing = 0,
        IgnoreMainTitle,
        IgnoreSourceTitle,
        IgnorePublishTitle,
        IgnoreMainAndSource,
        IgnoreMainAndPublish,
        IgnoreSourceAndPublish,
    }

    public interface IEPubConversionSettings : IXmlSerializable
    {
        void CopyFrom(IEPubConversionSettings temp);
        void SetupDefaults();

        ITransliterationSettings TransliterationSettings { get; set; }
        bool TransliterateFileName { get; set; }
        bool Fb2Info { get; set; }
        bool AddSeqToTitle { get; set; }
        string SequenceFormat { get; set; }
        string NoSequenceFormat { get; set; }
        string NoSeriesFormat { get; set; }
        string AuthorFormat { get; set; }
        string FileAsFormat { get; set; }
        bool SkipAboutPage { get; set; }
        IgnoreInfoSourceOptions IgnoreTitle { get; set; }
        IgnoreInfoSourceOptions IgnoreAuthors { get; set; }
        IgnoreInfoSourceOptions IgnoreTranslators { get; set; }
        IgnoreInfoSourceOptions IgnoreGenres { get; set; }
        bool DecorateFontNames { get; set; }
        IEPubFontSettings Fonts { get; set; }
    }
}
