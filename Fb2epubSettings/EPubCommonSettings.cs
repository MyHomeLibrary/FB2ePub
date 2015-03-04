using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ConverterContracts.Settings;
using FontSettingsContracts;
using FontsSettings;
namespace Fb2epubSettings
{
    public class EPubCommonSettings : IEPubCommonSettings , IXmlSerializable
    {
        #region private_members
        private bool _transliterate;
        private bool _transliterateFileName;
        private bool _transliterateTOC;
        private bool _addFB2Info;
        private bool _addSeqToTitle;
        private string _sequenceFormat = string.Empty;
        private string _noSequenceFormat = string.Empty;
        private string _noSeriesFormat = string.Empty;
        private bool _flatStructure;
        private bool _embedStyles;
        private string _authorFormat = string.Empty;
        private string _fileAsFormat = string.Empty;
        private bool _capitalDrop;
        private bool _skipAboutPage;
        private IgnoreInfoSourceOptions _ignoreTitle = IgnoreInfoSourceOptions.IgnoreNothing;
        private IgnoreInfoSourceOptions _ignoreAuthors= IgnoreInfoSourceOptions.IgnoreNothing;
        private IgnoreInfoSourceOptions _ignoreTranslators = IgnoreInfoSourceOptions.IgnoreNothing;
        private IgnoreInfoSourceOptions _ignoreGenres = IgnoreInfoSourceOptions.IgnoreNothing;
        private bool _decorateFontNames;
        private readonly EPubFontSettings _fonts = new EPubFontSettings();
        #endregion


        #region ElementNames

        public const string CommonSettingsElementName = "CommonSettings";

        private const string TransliterateElementName = "TransliterateBook";
        private const string TransliterateFileElementName = "TransliterateFileName";
        private const string TransliterateTOCElementName = "TransliterateTOC";
        private const string AddFB2InfoElementName = "AddFB2Info";
        private const string AddSequenceNameToTitleElementName = "AddSequenceNameToTitle";
        private const string FormatWithSequenceNameElementName = "FormatWithSequenceName";
        private const string FormatWithOutSequenceNameElementName = "FormatWithOutSequenceName";
        private const string FormatWithOutSeriesNameElementName = "FormatWithOutSeriesName";
        private const string CreateFlatInternalFolderStructureElementName = "CreateFlatInternalFolderStructure";
        private const string EmbedStylesIntoXHTMLElementName = "EmbedStylesIntoXHTML";
        private const string AuthorNameFormatElementName = "AuthorNameFormat";
        private const string FileAsFormatElementName = "FileAsFormat";
        private const string GenerateDropCharactersElementName = "GenerateDropCharacters";
        private const string SkipAboutPageGenerationElementName = "SkipAboutPageGeneration";
        private const string IgnoreTitleOptionElementName = "IgnoreTitleOption";
        private const string IgnoreAuthorsOptionElementName = "IgnoreAuthorsOption";
        private const string IgnoreTranstatorsOptionElementName = "IgnoreTranstatorsOption";
        private const string IgnoreGenresOptionElementName = "IgnoreGenresOption";
        private const string DecorateFontNamesElementName = "DecorateFontNames";

        #endregion

        public void CopyFrom(IEPubCommonSettings temp)
        {
            if (temp == null)
            {
                throw new ArgumentNullException("temp");
            }
            if (temp == this)
            {
                return;
            }
            _transliterate = temp.Transliterate;
            _transliterateFileName = temp.TransliterateFileName;
            _transliterateTOC = temp.TransliterateToc;
            _addFB2Info = temp.Fb2Info;
            _addSeqToTitle = temp.AddSeqToTitle;
            _sequenceFormat = temp.SequenceFormat;
            _noSequenceFormat = temp.NoSequenceFormat;
            _noSeriesFormat = temp.NoSeriesFormat;
            _flatStructure = temp.Flat;
            _embedStyles = temp.EmbedStyles;
            _authorFormat = temp.AuthorFormat;
            _fileAsFormat = temp.FileAsFormat;
            _capitalDrop = temp.CapitalDrop;
            _skipAboutPage = temp.SkipAboutPage;
            _ignoreTitle = temp.IgnoreTitle;
            _ignoreAuthors = temp.IgnoreAuthors;
            _ignoreTranslators = temp.IgnoreTranslators;
            _ignoreGenres = temp.IgnoreGenres;
            _decorateFontNames = temp.DecorateFontNames;
            _fonts.CopyFrom(temp.Fonts);
        }

        public void SetupDefaults()
        {

            _transliterate = false;
            _transliterateFileName = false;
            _transliterateTOC = false;
            _addFB2Info = true;
            _addSeqToTitle = true;
            _sequenceFormat = @"%bt% %sa.l%-%sn%";
            _noSequenceFormat = @"%bt% (%sf.l%)";
            _noSeriesFormat = @"%bt%";
            _flatStructure = false;
            _embedStyles = false;
            _authorFormat = @"%f.c% %m.c% %l.c% %n.c:b%";
            _fileAsFormat = @"%l.c% %f.c%";
            _capitalDrop = true;
            _skipAboutPage = false;
            _ignoreTitle = IgnoreInfoSourceOptions.IgnoreNothing;
            _ignoreAuthors = IgnoreInfoSourceOptions.IgnoreNothing;
            _ignoreTranslators = IgnoreInfoSourceOptions.IgnoreNothing;
            _ignoreGenres = IgnoreInfoSourceOptions.IgnoreNothing;
            _decorateFontNames = true;


            _fonts.FontFamilies.Clear();
            _fonts.CssElements.Clear();

            CSSFontFamily family = new CSSFontFamily() { Name = @"LiberationSerif" };

            CSSFont font1 = new CSSFont
            {
                FontStyle = FontStylesEnum.Normal,
                FontVariant = FontVaiantEnum.Normal,
                FontWidth = FontBoldnessEnum.B400,
                FontStretch = FontStretch.Normal
            };
            font1.Sources.Add(new FontSource() { Type = SourceTypes.Embedded, Format = FontFormat.Unknown, Location = @"%ResourceFolder%\Fonts/LiberationSerif-Regular.ttf" });
            family.Fonts.Add(font1);

            CSSFont font2 = new CSSFont
            {
                FontStyle = FontStylesEnum.Italic,
                FontVariant = FontVaiantEnum.Normal,
                FontWidth = FontBoldnessEnum.B400,
                FontStretch = FontStretch.Normal
            };
            font2.Sources.Add(new FontSource() { Type = SourceTypes.Embedded, Format = FontFormat.Unknown, Location = @"%ResourceFolder%\Fonts/LiberationSerif-Italic.ttf" });
            family.Fonts.Add(font2);

            CSSFont font3 = new CSSFont
            {
                FontStyle = FontStylesEnum.Normal,
                FontVariant = FontVaiantEnum.Normal,
                FontWidth = FontBoldnessEnum.B700,
                FontStretch = FontStretch.Normal
            };
            font3.Sources.Add(new FontSource() { Type = SourceTypes.Embedded, Format = FontFormat.Unknown, Location = @"%ResourceFolder%\Fonts/LiberationSerif-Bold.ttf" });
            family.Fonts.Add(font3);

            CSSFont font4 = new CSSFont
            {
                FontStyle = FontStylesEnum.Italic,
                FontVariant = FontVaiantEnum.Normal,
                FontWidth = FontBoldnessEnum.B700,
                FontStretch = FontStretch.Normal
            };
            font4.Sources.Add(new FontSource() { Type = SourceTypes.Embedded, Format = FontFormat.Unknown, Location = @"%ResourceFolder%\Fonts/LiberationSerif-BoldItalic.ttf" });
            family.Fonts.Add(font4);

            _fonts.FontFamilies.Add(family);

            
            
            CSSStylableElement css1 = new CSSStylableElement() { Name = "body" };
            css1.AssignedFontFamilies.Add(family.Name);
            _fonts.CssElements.Add(css1);

            CSSStylableElement css2 = new CSSStylableElement() { Name = "code" };
            css2.AssignedFontFamilies.Add(family.Name);
            _fonts.CssElements.Add(css2);

            CSSStylableElement css3 = new CSSStylableElement() { Name = "epub" };
            css3.AssignedFontFamilies.Add(family.Name);
            _fonts.CssElements.Add(css3);

        }



        #region serializable_public_Properties
        /// <summary>
        /// Get/Set if data outside the text body needs to be transliterated 
        /// (used in case device does not support Cyrillic fonts)
        /// </summary>
        public bool Transliterate
        {
            get { return _transliterate; }
            set { _transliterate = value; }
        }

        /// <summary>
        /// Get/Set transliteration of the output file name(s)
        /// </summary>
        public bool TransliterateFileName
        {
            get { return _transliterateFileName; }
            set { _transliterateFileName = value; }
        }

        /// <summary>
        /// Get set transliteration of TOC
        /// </summary>
        public bool TransliterateToc
        {
            get { return _transliterateTOC; }
            set { _transliterateTOC = value; }
        }

        /// <summary>
        /// Get/Set if program should generate FB2 info page
        /// </summary>
        public bool Fb2Info
        {
            get { return _addFB2Info; }
            set { _addFB2Info = value; }
        }

        /// <summary>
        /// Get/Set if sequences abbreviations should be added to title
        /// </summary>
        public bool AddSeqToTitle
        {
            get { return _addSeqToTitle; }
            set { _addSeqToTitle = value; }
        }

        /// <summary>
        /// Get/Set book title sequence format
        /// </summary>
        public string SequenceFormat
        {
            get { return _sequenceFormat; }
            set { _sequenceFormat = value; }
        }

        /// <summary>
        /// Get/Set book title with no sequence format
        /// </summary>
        public string NoSequenceFormat
        {
            get { return _noSequenceFormat; }
            set { _noSequenceFormat = value; }
        }

        /// <summary>
        /// Get/Set book title with no series format
        /// </summary>
        public string NoSeriesFormat
        {
            get { return _noSeriesFormat; }
            set { _noSeriesFormat = value; }
        }

        /// <summary>
        /// Get/Set "flat" mode , when flat mode is set no subfolders created inside the ZIP
        /// used to work around bugs in some readers
        /// </summary>
        public bool Flat
        {
            get { return _flatStructure; }
            set { _flatStructure = value; }
        }


        /// <summary>
        /// Get/Set embedding styles into xHTML files instead of referencing style files
        /// </summary>
        public bool EmbedStyles
        {
            get { return _embedStyles; }
            set { _embedStyles = value; }
        }

        /// <summary>
        /// Get/Set Author format string
        /// </summary>
        public string AuthorFormat
        {
            get { return _authorFormat; }
            set { _authorFormat = value; }
        }

        /// <summary>
        /// Get/Set FileAs format string
        /// </summary>
        public string FileAsFormat
        {
            get { return _fileAsFormat; }
            set { _fileAsFormat = value; }
        }

        /// <summary>
        /// Get set if a first character in section should start from capital huge "floating" character
        /// </summary>
        public bool CapitalDrop
        {
            get { return _capitalDrop; }
            set { _capitalDrop = value; }
        }

        /// <summary>
        /// Get/Set if About page generation will be skipped
        /// </summary>
        public bool SkipAboutPage
        {
            get { return _skipAboutPage; }
            set { _skipAboutPage = value; }
        }


        /// <summary>
        /// Controls which titles source should be ignored when generating ePub
        /// by default - nothing
        /// </summary>
        public IgnoreInfoSourceOptions IgnoreTitle
        {
            get { return _ignoreTitle; }
            set { _ignoreTitle = value; }
        }

        /// <summary>
        /// Controls which Authors source should be ignored when generating ePub
        /// by default - nothing
        /// </summary>
        public IgnoreInfoSourceOptions IgnoreAuthors
        {
            get { return _ignoreAuthors; }
            set { _ignoreAuthors = value; }
        }

        /// <summary>
        /// Controls which Translators source should be ignored when generating ePub
        /// by default - nothing
        /// </summary>
        public IgnoreInfoSourceOptions IgnoreTranslators
        {
            get { return _ignoreTranslators; }
            set { _ignoreTranslators= value; }
        }

        /// <summary>
        /// Controls which Genres source should be ignored when generating ePub
        /// by default - nothing
        /// </summary>
        public IgnoreInfoSourceOptions IgnoreGenres
        {
            get { return _ignoreGenres; }
            set { _ignoreGenres = value; }
        }

        /// <summary>
        /// Get/Set if font names should be decorated to work around adobe memory cache issue
        /// </summary>
        public bool DecorateFontNames
        {
            get { return _decorateFontNames; }
            set { _decorateFontNames = value; }
        }

        /// <summary>
        /// Get/Set Fonts settings
        /// </summary>
        public IEPubFontSettings Fonts
        {
            get { return _fonts; }
            set { _fonts.CopyFrom(value); }
        }

        #endregion

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case TransliterateElementName:
                            _transliterate = reader.ReadElementContentAsBoolean();
                            continue;
                        case TransliterateFileElementName:
                            _transliterateFileName = reader.ReadElementContentAsBoolean();
                            continue;
                        case TransliterateTOCElementName:
                            _transliterateTOC = reader.ReadElementContentAsBoolean();
                            continue;
                        case AddFB2InfoElementName:
                            _addFB2Info = reader.ReadElementContentAsBoolean();
                            continue;
                        case AddSequenceNameToTitleElementName:
                            _addSeqToTitle = reader.ReadElementContentAsBoolean();
                            continue;
                        case FormatWithSequenceNameElementName:
                            _sequenceFormat = reader.ReadElementContentAsString();
                            continue;
                        case FormatWithOutSequenceNameElementName:
                            _noSequenceFormat = reader.ReadElementContentAsString();
                            continue;
                        case FormatWithOutSeriesNameElementName:
                            _noSeriesFormat = reader.ReadElementContentAsString();
                            continue;
                        case CreateFlatInternalFolderStructureElementName:
                            _flatStructure = reader.ReadElementContentAsBoolean();
                            continue;
                        case EmbedStylesIntoXHTMLElementName:
                            _embedStyles = reader.ReadElementContentAsBoolean();
                            continue;
                        case AuthorNameFormatElementName:
                            _authorFormat = reader.ReadElementContentAsString();
                            continue;
                        case FileAsFormatElementName:
                            _fileAsFormat = reader.ReadElementContentAsString();
                            continue;
                        case GenerateDropCharactersElementName:
                            _capitalDrop = reader.ReadElementContentAsBoolean();
                            continue;
                        case SkipAboutPageGenerationElementName:
                            _skipAboutPage = reader.ReadElementContentAsBoolean();
                            continue;
                        case IgnoreTitleOptionElementName:
                            IgnoreInfoSourceOptions ignoreTitle;
                            string elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out ignoreTitle))
                            {
                                throw new InvalidDataException(string.Format("Invalit ignore Title value read: {0}",elementContent));
                            }
                            _ignoreTitle = ignoreTitle;
                            continue;
                        case IgnoreAuthorsOptionElementName:
                            IgnoreInfoSourceOptions ignoreAuthors;
                            elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out ignoreAuthors))
                            {
                                throw new InvalidDataException(string.Format("Invalit ignore Authors value read: {0}", elementContent));
                            }
                            _ignoreAuthors = ignoreAuthors;
                            continue;
                        case IgnoreTranstatorsOptionElementName:
                            IgnoreInfoSourceOptions ignoreTranslators;
                            elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out ignoreTranslators))
                            {
                                throw new InvalidDataException(string.Format("Invalit ignore Translators value read: {0}", elementContent));
                            }
                            _ignoreTranslators = ignoreTranslators;
                            continue;
                        case IgnoreGenresOptionElementName:
                            IgnoreInfoSourceOptions ignoreGenres;
                            elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out ignoreGenres))
                            {
                                throw new InvalidDataException(string.Format("Invalit ignore Genres value read: {0}", elementContent));
                            }
                            _ignoreGenres = ignoreGenres;
                            continue;
                        case DecorateFontNamesElementName:
                            _decorateFontNames = reader.ReadElementContentAsBoolean();
                            continue;
                        case EPubFontSettings.FontsElementName:
                            _fonts.ReadXml(reader.ReadSubtree());
                            break;
                    }
                }
                reader.Read();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(CommonSettingsElementName);

            writer.WriteStartElement(TransliterateElementName);
            writer.WriteValue(_transliterate);
            writer.WriteEndElement();

            writer.WriteStartElement(TransliterateFileElementName);
            writer.WriteValue(_transliterateFileName);
            writer.WriteEndElement();


            writer.WriteStartElement(TransliterateTOCElementName);
            writer.WriteValue(_transliterateTOC);
            writer.WriteEndElement();

            writer.WriteStartElement(AddFB2InfoElementName);
            writer.WriteValue(_addFB2Info);
            writer.WriteEndElement();

            writer.WriteStartElement(AddSequenceNameToTitleElementName);
            writer.WriteValue(_addSeqToTitle);
            writer.WriteEndElement();

            writer.WriteStartElement(FormatWithSequenceNameElementName);
            writer.WriteValue(_sequenceFormat);
            writer.WriteEndElement();

            writer.WriteStartElement(FormatWithOutSequenceNameElementName);
            writer.WriteValue(_noSequenceFormat);
            writer.WriteEndElement();

            writer.WriteStartElement(FormatWithOutSeriesNameElementName);
            writer.WriteValue(_noSeriesFormat);
            writer.WriteEndElement();

            writer.WriteStartElement(CreateFlatInternalFolderStructureElementName);
            writer.WriteValue(_flatStructure);
            writer.WriteEndElement();

            writer.WriteStartElement(EmbedStylesIntoXHTMLElementName);
            writer.WriteValue(_embedStyles);
            writer.WriteEndElement();

            writer.WriteStartElement(AuthorNameFormatElementName);
            writer.WriteValue(_authorFormat);
            writer.WriteEndElement();

            writer.WriteStartElement(FileAsFormatElementName);
            writer.WriteValue(_fileAsFormat);
            writer.WriteEndElement();

            writer.WriteStartElement(GenerateDropCharactersElementName);
            writer.WriteValue(_capitalDrop);
            writer.WriteEndElement();

            writer.WriteStartElement(SkipAboutPageGenerationElementName);
            writer.WriteValue(_skipAboutPage);
            writer.WriteEndElement();

            writer.WriteStartElement(IgnoreTitleOptionElementName);
            writer.WriteValue(_ignoreTitle.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(IgnoreAuthorsOptionElementName);
            writer.WriteValue(_ignoreAuthors.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(IgnoreTranstatorsOptionElementName);
            writer.WriteValue(_ignoreTranslators.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(IgnoreGenresOptionElementName);
            writer.WriteValue(_ignoreGenres.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(DecorateFontNamesElementName);
            writer.WriteValue(_decorateFontNames);
            writer.WriteEndElement();

            _fonts.WriteXml(writer);

            writer.WriteEndElement();
        }
    }
}
