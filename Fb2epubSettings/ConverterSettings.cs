using Fb2epubSettings.AppleSettings;
using FontsSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Fb2epubSettings
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

    public enum EPubVersion
    {
        VEpub20 =   2,
        VePub30 =   3,
    }

    [XmlRoot(ElementName = "ConverterSettings")]
    public class ConverterSettings
    {
        #region private_members
        private string _outputPath = string.Empty;
        private bool _transliterate = false;
        private bool _transliterateFileName = false;
        private bool _transliterateTOC = false;
        private bool _addFB2Info = false;
        private FixOptions _fixMode = FixOptions.DoNotFix;
        private bool _addSeqToTitle = false;
        private string _sequenceFormat = string.Empty;
        private string _noSequenceFormat = string.Empty;
        private string _noSeriesFormat = string.Empty;
        private bool _flatStructure = false;
        private bool _convertAlphaPng = false;
        private bool _embedStyles = false;
        private string _authorFormat = string.Empty;
        private string _fileAsFormat = string.Empty;
        private bool _capitalDrop = false;
        private bool _skipAboutPage = false;
        private bool _enableAdobeTemplate = false;
        private IgnoreTitleOptions _ignoreTitle = IgnoreTitleOptions.IgnoreNothing;
        private string _adobeTemplatePath = string.Empty;
        private bool _decorateFontNames = false;
        private readonly EPubFontSettings _fonts = new EPubFontSettings();
        private string _resourcesPath = string.Empty;
        private readonly AppleConverterSettings _appleEPubSettings = new AppleConverterSettings();
        private bool _addCalibreMetadata = true;
        private bool _fixCodeElement = true;
        private EPubVersion _standardVersion = EPubVersion.VEpub20;
        #endregion

        public ConverterSettings()
        {
            SetupDefaults();
        }

        /// <summary>
        /// Creates default settings set
        /// </summary>
        public void SetupDefaults()
        {
            _transliterate = false;
            _transliterateFileName = false;
            _transliterateTOC = false;
            _addFB2Info = true;
            _fixMode = FixOptions.UseFb2Fix;
            _addSeqToTitle = true;
            _sequenceFormat = @"%bt% %sa.l%-%sn%";
            _noSequenceFormat = @"%bt% (%sf.l%)";
            _noSeriesFormat = @"%bt%";
            _flatStructure = false;
            _convertAlphaPng = true;
            _embedStyles = false;
            _authorFormat = @"%f.c%%m.c%%l.c%%n.c:b%";
            _fileAsFormat = @"%l.c%%f.c%";
            _capitalDrop = true;
            _skipAboutPage = false;
            _enableAdobeTemplate = false;
            _ignoreTitle = IgnoreTitleOptions.IgnoreNothing;
            _adobeTemplatePath = string.Empty;
            _decorateFontNames = true;
            _resourcesPath = string.Empty;
            _addCalibreMetadata = true;
            _fixCodeElement = true;
            _appleEPubSettings.SetupDefaults();

            _fonts.FontFamilies.Clear();
            //CSSFontFamily family = new CSSFontFamily() { Name = @"LiberationSerif" };
            
            //CSSFont font1 = new CSSFont();
            //font1.FontStyle = FontStylesEnum.Normal;
            //font1.FontVariant = FontVaiantEnum.Normal;
            //font1.FontWidth = FontBoldnessEnum.B400;
            //font1.FontStretch = FontStretch.Normal;
            //font1.Sources.Add(new FontSource() { Type = SourceTypes.Embedded, Format = FontFormat.Unknown, Location = @"%ResourceFolder%\Fonts/LiberationSerif-Regular.ttf" });
            //family.Fonts.Add(font1);

            //CSSFont font2 = new CSSFont();
            //font2.FontStyle = FontStylesEnum.Italic;
            //font2.FontVariant = FontVaiantEnum.Normal;
            //font2.FontWidth = FontBoldnessEnum.B400;
            //font2.FontStretch = FontStretch.Normal;
            //font2.Sources.Add(new FontSource() { Type = SourceTypes.Embedded, Format = FontFormat.Unknown, Location = @"%ResourceFolder%\Fonts/LiberationSerif-Italic.ttf" });
            //family.Fonts.Add(font2);

            //CSSFont font3 = new CSSFont();
            //font3.FontStyle = FontStylesEnum.Normal;
            //font3.FontVariant = FontVaiantEnum.Normal;
            //font3.FontWidth = FontBoldnessEnum.B700;
            //font3.FontStretch = FontStretch.Normal;
            //font3.Sources.Add(new FontSource() { Type = SourceTypes.Embedded, Format = FontFormat.Unknown, Location = @"%ResourceFolder%\Fonts/LiberationSerif-Bold.ttf" });
            //family.Fonts.Add(font3);

            //CSSFont font4 = new CSSFont();
            //font4.FontStyle = FontStylesEnum.Italic;
            //font4.FontVariant = FontVaiantEnum.Normal;
            //font4.FontWidth = FontBoldnessEnum.B700;
            //font4.FontStretch = FontStretch.Normal;
            //font4.Sources.Add(new FontSource() { Type = SourceTypes.Embedded, Format = FontFormat.Unknown, Location = @"%ResourceFolder%\Fonts/LiberationSerif-BoldItalic.ttf" });
            //family.Fonts.Add(font4);

            //_fonts.FontFamilies.Add(family);



            _fonts.CssElements.Clear();

            //CSSStylableElement css1 = new CSSStylableElement() { Name = "body" };
            //css1.AssignedFontFamilies.Add(family.Name);
            //_fonts.CssElements.Add(css1);

            //CSSStylableElement css2 = new CSSStylableElement() { Name = "code" };
            //css2.AssignedFontFamilies.Add(family.Name);
            //_fonts.CssElements.Add(css2);

            //CSSStylableElement css3 = new CSSStylableElement() { Name = "epub" };
            //css3.AssignedFontFamilies.Add(family.Name);
            //_fonts.CssElements.Add(css3);

            _standardVersion = EPubVersion.VePub30;

        }

        #region serializable_public_Properties

        /// <summary>
        /// Get/Set if data outside the text body needs to be transliterated 
        /// (used in case device does not support Cyrillic fonts)
        /// </summary>
        [XmlElement(ElementName = "TransliterateBook")]
        public bool Transliterate 
        {
            get { return _transliterate; }
            set { _transliterate = value; }
        }

        /// <summary>
        /// Get/Set if Calibre metadata should be added
        /// </summary>
        [XmlElement(ElementName = "AddCalibreMetadata")]
        public bool AddCalibreMetadata
        {
            get { return _addCalibreMetadata; }
            set { _addCalibreMetadata = value; }
        }

        /// <summary>
        /// Get/Set transliteration of the output file name(s)
        /// </summary>
        [XmlElement(ElementName = "TransliterateFileName")]
        public bool TransliterateFileName 
        {
            get { return _transliterateFileName; }
            set { _transliterateFileName = value; }
        }

        /// <summary>
        /// Get set transliteration of TOC
        /// </summary>
        [XmlElement(ElementName = "TransliterateTOC")]
        public bool TransliterateToc
        {
            get { return _transliterateTOC; }
            set { _transliterateTOC = value; }
        }

        /// <summary>
        /// Get/Set if program should generate FB2 info page
        /// </summary>
        [XmlElement(ElementName = "AddFB2Info")]
        public bool Fb2Info 
        {
            get { return _addFB2Info; }
            set { _addFB2Info = value; } 
        }

        /// <summary>
        /// Get/Set FB2 fix mode
        /// </summary>
        [XmlElement(ElementName = "Fb2FixMode")]
        public FixOptions FixMode 
        {
            get { return _fixMode; }
            set { _fixMode = value; }
        }

        /// <summary>
        /// Get/Set if sequences abbreviations should be added to title
        /// </summary>
        [XmlElement(ElementName = "AddSequenceNameToTitle")]
        public bool AddSeqToTitle 
        {
            get { return _addSeqToTitle; }
            set { _addSeqToTitle = value; }
        }

        /// <summary>
        /// Get/Set book title sequence format
        /// </summary>
        [XmlElement(ElementName = "FormatWithSequenceName")]
        public string SequenceFormat 
        {
            get { return _sequenceFormat; }
            set { _sequenceFormat = value; } 
        }

        /// <summary>
        /// Get/Set book title with no sequence format
        /// </summary>
        [XmlElement(ElementName = "FormatWithOutSequenceName")]
        public string NoSequenceFormat 
        {
            get { return _noSequenceFormat; }
            set { _noSequenceFormat = value; }
        }

        /// <summary>
        /// Get/Set book title with no series format
        /// </summary>
        [XmlElement(ElementName = "FormatWithOutSeriesName")]
        public string NoSeriesFormat 
        {
            get { return _noSeriesFormat; }
            set { _noSeriesFormat = value; } 
        }

        /// <summary>
        /// Get/Set "flat" mode , when flat mode is set no subfolders created inside the ZIP
        /// used to work around bugs in some readers
        /// </summary>
        [XmlElement(ElementName = "CreateFlatInternalFolderStructure")]
        public bool Flat 
        {
            get { return _flatStructure; }
            set { _flatStructure = value; } 
        }

        /// <summary>
        /// Set/Get if alpha channel palette bitmaps need to be converted
        /// </summary>
        [XmlElement(ElementName = "ConvertAlphaChannelPNGImages")]
        public bool ConvertAlphaPng 
        {
            get { return _convertAlphaPng; }
            set { _convertAlphaPng = value; }
        }

        /// <summary>
        /// Get/Set embedding styles into xHTML files instead of referencing style files
        /// </summary>
        [XmlElement(ElementName = "EmbedStylesIntoXHTML")]
        public bool EmbedStyles 
        {
            get { return _embedStyles; }
            set { _embedStyles = value; }
        }

        /// <summary>
        /// Get/Set Author format string
        /// </summary>
        [XmlElement(ElementName = "AuthorNameFormat")]
        public string AuthorFormat 
        {
            get { return _authorFormat; }
            set { _authorFormat = value; }
        }

        /// <summary>
        /// Get/Set FileAs format string
        /// </summary>
        [XmlElement(ElementName = "FileAsFormat")]
        public string FileAsFormat 
        {
            get { return _fileAsFormat; }
            set { _fileAsFormat = value; } 
        }

        /// <summary>
        /// Get set if a first character in section should start from capital huge "floating" character
        /// </summary>
        [XmlElement(ElementName = "GenerateDropCharacters")]
        public bool CapitalDrop 
        {
            get { return _capitalDrop; }
            set { _capitalDrop = value; }
        }

        /// <summary>
        /// Get/Set if About page generation will be skipped
        /// </summary>
        [XmlElement(ElementName = "SkipAboutPageGeneration")]
        public bool SkipAboutPage 
        {
            get { return _skipAboutPage; }
            set { _skipAboutPage = value; }
        }

        /// <summary>
        /// Enable/Disable embedding of Adobe XPGT Template into a resulting file
        /// </summary>
        [XmlElement(ElementName = "EnableAdobeTemplateUsage")]
        public bool EnableAdobeTemplate 
        {
            get { return _enableAdobeTemplate; }
            set { _enableAdobeTemplate = value; }
        }


        /// <summary>
        /// Controls which titles should be ignored when generating ePub
        /// by default - nothing
        /// </summary>
        [XmlElement(ElementName = "IgnoreTitleOption")]
        public IgnoreTitleOptions IgnoreTitle 
        {
            get { return _ignoreTitle; }
            set { _ignoreTitle = value; } 
        }

        /// <summary>
        /// Path to location of Adobe XPGT template
        /// </summary>
        [XmlElement(ElementName = "AdobeTemplatePath")]
        public string AdobeTemplatePath 
        {
            get { return _adobeTemplatePath; }
            set { _adobeTemplatePath = value; }
        }

        /// <summary>
        /// Get/Set if font names should be decorated to work around adobe memory cache bug
        /// </summary>
        [XmlElement(ElementName = "DecorateFontNames")]
        public bool DecorateFontNames 
        {
            get { return _decorateFontNames; }
            set { _decorateFontNames = value; }
        }

        /// <summary>
        /// Get/Set Fonts settings
        /// </summary>
        [XmlElement(ElementName = "Fonts")]
        public EPubFontSettings Fonts 
        {
            get { return _fonts; }
            set { _fonts.CopyFrom(value); }
        }


        /// <summary>
        /// Get/Set path to conversion resources location (CSS, fonts etc)
        /// </summary>
        [XmlElement(ElementName = "ResourcesPath")]
        public string ResourcesPath 
        {
            get { return _resourcesPath; }
            set { _resourcesPath = value; }
        }

        /// <summary>
        /// Return reference to set of apple/iBook related settings
        /// </summary>
        [XmlElement(ElementName = "AppleConverterEPub2Settings")]
        public AppleConverterSettings AppleConverterEPubSettings
        {
            get { return _appleEPubSettings; }
            set { _appleEPubSettings.CopyFrom(value); }
        }

        /// <summary>
        /// Version of ePub to generate
        /// </summary>
        [XmlElement(ElementName = "EPUBVersion")]
        public EPubVersion StandardVersion
        {
            get { return _standardVersion; }
            set { _standardVersion = value; }
        }

        #endregion

        #region nonserializable_public_Properties

        /// <summary>
        /// Get/Set output path used in case output file name does not includes path
        /// </summary>
        [XmlIgnore]
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
        #endregion



        public void CopyFrom(ConverterSettings temp)
        {
            if (temp == null)
            {
                throw new ArgumentNullException("temp");
            }
            if (temp == this)
            {
                return;
            }

            _outputPath = temp._outputPath;
            _transliterate = temp._transliterate;
            _transliterateFileName = temp._transliterateFileName;
            _transliterateTOC = temp._transliterateTOC;
            _addFB2Info = temp._addFB2Info;
            _fixMode = temp._fixMode;
            _addSeqToTitle = temp._addSeqToTitle;
            _sequenceFormat = temp._sequenceFormat;
            _noSequenceFormat = temp._noSequenceFormat;
            _noSeriesFormat = temp._noSeriesFormat;
            _flatStructure = temp._flatStructure;
            _convertAlphaPng = temp._convertAlphaPng;
            _embedStyles = temp._embedStyles;
            _authorFormat = temp._authorFormat;
            _fileAsFormat = temp._fileAsFormat;
            _capitalDrop = temp._capitalDrop;
            _skipAboutPage = temp._skipAboutPage;
            _enableAdobeTemplate = temp._enableAdobeTemplate;
            _ignoreTitle = temp._ignoreTitle;
            _adobeTemplatePath = temp._adobeTemplatePath;
            _decorateFontNames = temp._decorateFontNames;
            _fonts.CopyFrom(temp._fonts);
            _resourcesPath = temp._resourcesPath;
            _addCalibreMetadata = temp._addCalibreMetadata;
            _fixCodeElement = temp._fixCodeElement;
            _appleEPubSettings.CopyFrom(temp._appleEPubSettings);
            _standardVersion = temp._standardVersion;
        }
    }
}
