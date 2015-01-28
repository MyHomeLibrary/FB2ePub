﻿using FontSettingsContracts;

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

    public interface IEPubCommonSettings
    {
        void CopyFrom(IEPubCommonSettings temp);
        void SetupDefaults();

        bool Transliterate { get; set; }
        bool TransliterateFileName { get; set; }
        bool TransliterateToc { get; set; }
        bool Fb2Info { get; set; }
        bool AddSeqToTitle { get; set; }
        string SequenceFormat { get; set; }
        string NoSequenceFormat { get; set; }
        string NoSeriesFormat { get; set; }
        bool Flat { get; set; }
        bool EmbedStyles { get; set; }
        string AuthorFormat { get; set; }
        string FileAsFormat { get; set; }
        bool CapitalDrop { get; set; }
        bool SkipAboutPage { get; set; }
        IgnoreInfoSourceOptions IgnoreTitle { get; set; }
        IgnoreInfoSourceOptions IgnoreAuthors { get; set; }
        IgnoreInfoSourceOptions IgnoreTranslators { get; set; }
        IgnoreInfoSourceOptions IgnoreGenres { get; set; }
        bool DecorateFontNames { get; set; }
        IEPubFontSettings Fonts { get; set; }
    }
}
