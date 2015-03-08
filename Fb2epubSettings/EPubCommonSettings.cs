using System.Xml;
using System.Xml.Schema;
using EPubLibraryContracts.Settings;

namespace Fb2epubSettings
{
    public class EPubCommonSettings : IEPubCommonSettings
    {
        private bool _transliterateTOC;


        #region constants
        public const string CommonSettingsElementName = "CommonSettings";

        private const string TransliterateTOCElementName = "TransliterateTOC";
        #endregion


        public void CopyFrom(IEPubCommonSettings temp)
        {
            _transliterateTOC = temp.TransliterateToc;
            
        }

        public void SetupDefaults()
        {
            _transliterateTOC = false;
        }

        /// <summary>
        /// Get set transliteration of TOC
        /// </summary>
        public bool TransliterateToc
        {
            get { return _transliterateTOC; }
            set { _transliterateTOC = value; }
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
                    }
                }
                reader.Read();
            }
        }


        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(CommonSettingsElementName);

            writer.WriteStartElement(TransliterateTOCElementName);
            writer.WriteValue(_transliterateTOC);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

    }
}
