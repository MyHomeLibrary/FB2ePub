using System.Xml;
using System.Xml.Schema;
using EPubLibraryContracts.Settings;

namespace Fb2epubSettings
{
    public class EPubCommonSettings : IEPubCommonSettings
    {
        private bool _transliterateTOC;
        private bool _flatFileFolderStructure;
        private bool _embeddStyles;
        private bool _capitalDrop;


        #region constants
        public const string CommonSettingsElementName = "CommonSettings";

        private const string TransliterateTOCElementName = "TransliterateTOC";
        private const string FlatFileFolderStructureElementName = "FlatFileFolderStructure";
        private const string EmbedStylesIntoXHTMLElementName = "EmbedStylesIntoXHTML";
        private const string GenerateDropCharactersElementName = "GenerateDropCharacters";


        #endregion


        public void CopyFrom(IEPubCommonSettings temp)
        {
            _transliterateTOC = temp.TransliterateToc;
            _flatFileFolderStructure = temp.FlatStructure;
            _embeddStyles = temp.EmbedStyles;
            _capitalDrop = temp.CapitalDrop;

        }

        public void SetupDefaults()
        {
            _transliterateTOC = false;
            _flatFileFolderStructure = false;
            _embeddStyles = false;
            _capitalDrop = true;
        }

        /// <summary>
        /// Get set transliteration of TOC
        /// </summary>
        public bool TransliterateToc
        {
            get { return _transliterateTOC; }
            set { _transliterateTOC = value; }
        }

        public bool FlatStructure
        {
            get { return _flatFileFolderStructure; }
            set { _flatFileFolderStructure = value; }
        }

        public bool EmbedStyles
        {
            get { return _embeddStyles; }
            set { _embeddStyles = value; }
        }


        /// <summary>
        /// Get set if a first character in section should start from capital huge "floating" character
        /// </summary>
        public bool CapitalDrop
        {
            get { return _capitalDrop; }
            set { _capitalDrop = value; }
        }


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
                        case TransliterateTOCElementName:
                            _transliterateTOC = reader.ReadElementContentAsBoolean();
                            continue;
                        case FlatFileFolderStructureElementName:
                            _flatFileFolderStructure = reader.ReadElementContentAsBoolean();
                            continue;
                        case EmbedStylesIntoXHTMLElementName:
                            _embeddStyles = reader.ReadElementContentAsBoolean();
                            continue;
                        case GenerateDropCharactersElementName:
                            _capitalDrop = reader.ReadElementContentAsBoolean();
                            continue;
                    }
                }
                reader.Read();
            }
        }


        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(CommonSettingsElementName);

            writer.WriteStartElement(TransliterateTOCElementName);
            writer.WriteValue(_transliterateTOC.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(FlatFileFolderStructureElementName);
            writer.WriteValue(_flatFileFolderStructure.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(EmbedStylesIntoXHTMLElementName);
            writer.WriteValue(_embeddStyles);
            writer.WriteEndElement();

            writer.WriteStartElement(GenerateDropCharactersElementName);
            writer.WriteValue(_capitalDrop);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

    }
}
