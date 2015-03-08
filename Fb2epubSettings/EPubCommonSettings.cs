using System.Xml;
using System.Xml.Schema;
using EPubLibraryContracts.Settings;

namespace Fb2epubSettings
{
    public class EPubCommonSettings : IEPubCommonSettings
    {
        private bool _transliterateTOC;
        private bool _flatFileFolderStructure;


        #region constants
        public const string CommonSettingsElementName = "CommonSettings";

        private const string TransliterateTOCElementName = "TransliterateTOC";
        private const string FlatFileFolderStructureElementName = "FlatFileFolderStructure";
        #endregion


        public void CopyFrom(IEPubCommonSettings temp)
        {
            _transliterateTOC = temp.TransliterateToc;
            _flatFileFolderStructure = temp.FlatStructure;

        }

        public void SetupDefaults()
        {
            _transliterateTOC = false;
            _flatFileFolderStructure = false;
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

            writer.WriteEndElement();
        }

    }
}
