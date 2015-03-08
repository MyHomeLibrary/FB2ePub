using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using EPubLibraryContracts.Settings;

namespace Fb2epubSettings
{
    public class EPubV3Settings : IEPubV3Settings 
    {
        private EPubV3SubStandard _v3SubStandard = EPubV3SubStandard.V30;
        private bool _generateV2CompatibleTOC = true;

        public EPubV3Settings()
        {
            HTMLFileMaxSize = 0;
        }

        #region constant

        public const string EPubV3SettingsElementName = "EPubV3Settings";

        private const string EPUB3SubVersionElementName = "EPUB3SubVersion";
        private const string GenerateV2CompatibleTOCElementName = "GenerateV2CompatibleTOC";
        private const string HTMLFileMaxSizeAllowedElementName = "HTMLFileMaxSizeAllowed";

        #endregion

        public void SetupDefaults()
        {
            _v3SubStandard = EPubV3SubStandard.V30;
            _generateV2CompatibleTOC = true;
            HTMLFileMaxSize = 0;
        }

        /// <summary>
        /// Variant (revision) of V3 standard 
        /// </summary>
        public EPubV3SubStandard V3SubStandard
        {
            get { return _v3SubStandard; }
            set { _v3SubStandard = value; }
        }

        public bool GenerateV2CompatibleTOC
        {
            get { return _generateV2CompatibleTOC; }
            set { _generateV2CompatibleTOC = value; }
        }

        public ulong HTMLFileMaxSize { get; set; }


        public void CopyFrom(IEPubV3Settings temp)
        {
            _v3SubStandard = temp.V3SubStandard;
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
                        case EPUB3SubVersionElementName:
                            EPubV3SubStandard standard;
                            string elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out standard))
                            {
                                throw new InvalidDataException(string.Format("Invalid epub standard version passed : {0}", elementContent));
                            }
                            _v3SubStandard = standard;
                            continue;
                        case GenerateV2CompatibleTOCElementName:
                            _generateV2CompatibleTOC = reader.ReadElementContentAsBoolean();
                            continue;
                        case HTMLFileMaxSizeAllowedElementName:
                            HTMLFileMaxSize = (ulong)reader.ReadElementContentAsLong();
                            continue;
                    }
                }
                reader.Read();
            }
           
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(EPubV3SettingsElementName);

            writer.WriteStartElement(EPUB3SubVersionElementName);
            writer.WriteValue(_v3SubStandard.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(GenerateV2CompatibleTOCElementName);
            writer.WriteValue(_generateV2CompatibleTOC.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(HTMLFileMaxSizeAllowedElementName);
            writer.WriteValue(HTMLFileMaxSize.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
