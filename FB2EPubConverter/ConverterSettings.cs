using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fb2ePubConverter;
using FontsSettings;

namespace FB2EPubConverter
{
    /// <summary>
    /// Fix mode options
    /// </summary>
    public enum FixOptions
    {
        DoNotFix, // do not attempt to fix
        MinimalFix, // try minimal (internal) fix
        UseFb2Fix, // use Fb2Fix if prev. failed
        Fb2FixAlways, // always use Fb2Fix
    }

    public enum IgnoreTitleOptions
    {
        IgnoreNothing = 0,
        IgnoreMainTitle,
        IgnoreSourceTitle,
        IgnorePublishTitle,
        IgnoreMainAndSource,
        IgnoreMainAndPublish,
        IgnoreSourceAndPublish,
    }


    public class ConverterSettings
    {
        private string _outputPath = string.Empty;

        /// <summary>
        /// Get/Set if data outside the text body needs to be transliterated 
        /// (used in case device does not support cyrillic fonts)
        /// </summary>
        public bool Transliterate { get; set; }

        /// <summary>
        /// Get/Set transliteration of the output file name(s)
        /// </summary>
        public bool TransliterateFileName { get; set; }

        /// <summary>
        /// Get set transliteration of TOC
        /// </summary>
        public bool TransliterateToc { get; set; }

        /// <summary>
        /// Get/Set if program should generate FB2 info page
        /// </summary>
        public bool Fb2Info { get; set; }

        /// <summary>
        /// Get/Set FB2 fix mode
        /// </summary>
        public FixOptions FixMode { get; set; }

        /// <summary>
        /// Get/Set if sequences abbreviations should be added to title
        /// </summary>
        public bool AddSeqToTitle { get; set; }

        /// <summary>
        /// Get/Set book title sequence format
        /// </summary>
        public string SequenceFormat { get; set; }

        /// <summary>
        /// Get/Set book title with no sequence format
        /// </summary>
        public string NoSequenceFormat { get; set; }

        /// <summary>
        /// Get/Set book title with no series format
        /// </summary>
        public string NoSeriesFormat { get; set; }

        /// <summary>
        /// Get/Set "flat" mode , when flat mode is set no subfolders created inside the ZIP
        /// used to work aroun bugs in some readers
        /// </summary>
        public bool Flat { get; set; }

        /// <summary>
        /// Set/Get if alpha channel palette bitmaps need to be converted
        /// </summary>
        public bool ConvertAlphaPng { get; set; }

        /// <summary>
        /// Get/Set embeding styles into xHTML files instead of referencing style files
        /// </summary>
        public bool EmbedStyles { get; set; }

        /// <summary>
        /// Get/Set Author format string
        /// </summary>
        public string AuthorFormat { get; set; }

        /// <summary>
        /// Get/Set FileAs format string
        /// </summary>
        public string FileAsFormat { get; set; }

        /// <summary>
        /// Get set if a first character in section should start from capital huge "floating" character
        /// </summary>
        public bool CapitalDrop { get; set; }

        /// <summary>
        /// Get/Set if About page generation will be skipped
        /// </summary>
        public bool SkipAboutPage { get; set; }

        /// <summary>
        /// Enable/Disable embeding of Adobe XPGT Template into a resulting file
        /// </summary>
        public bool EnableAdobeTemplate { get; set; }


        /// <summary>
        /// Controls which titles should be ignored when generating ePub
        /// by default - nothing
        /// </summary>
        public IgnoreTitleOptions IgnoreTitle { get; set; }

        /// <summary>
        /// Path to location of Adobe XPGT template
        /// </summary>
        public string AdobeTemplatePath { get; set; }

        /// <summary>
        /// Get/Set if font names should be decorated to work around adobe memory cache bug
        /// </summary>
        public bool DecorateFontNames { get; set; }

        /// <summary>
        /// Get/Set Fonts settings
        /// </summary>
        public EPubFontSettings Fonts { get; set; }


        /// <summary>
        /// Get/Set path to conversion resources location (css, fonts etc)
        /// </summary>
        public string ResourcesPath { get; set; }

        /// <summary>
        /// Get/Set output path used in case output file name does not includes path
        /// </summary>
        public string OutPutPath
        {
            get { return _outputPath; }
            set 
            { 
                _outputPath = value;
                if (!_outputPath.EndsWith("\\") && !_outputPath.EndsWith("/") && !string.IsNullOrEmpty(_outputPath))
                {
                    _outputPath += "\\";
                }

            }
        }



    }
}
