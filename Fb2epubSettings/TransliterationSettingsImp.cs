using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using TranslitRuContracts;

namespace Fb2epubSettings
{


    public class TransliterationSettingsImp : ITransliterationSettings
    {
        public TranslitModeEnum Mode { get; set; }
        public string FileName { get; set; }


        #region constants
        public const string TransliterationSettingsElementName = "TransliterationSettings";

        private const string ModeElementName = "Mode";
        private const string FileNameElementName = "FileName";
        #endregion

        public bool Transliterate
        {
            get { return Mode != TranslitModeEnum.None; }
        }

        public void CopyFrom(ITransliterationSettings temp)
        {
            Mode = temp.Mode;
            FileName = temp.FileName;
        }

        public void SetupDefaults()
        {
            Mode = TranslitModeEnum.None;
            FileName = string.Empty;
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
                        case ModeElementName:
                            TranslitModeEnum mode;
                            string elementContent = reader.ReadElementContentAsString();
                            if (!Enum.TryParse(elementContent, true, out mode))
                            {
                                throw new InvalidDataException(string.Format("Invalit ignore Title value read: {0}",elementContent));
                            }
                            Mode = mode;
                            continue;
                        case FileNameElementName:
                            FileName = reader.ReadElementContentAsString();
                            continue;
                    }
                }
                reader.Read();
            }
            
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(TransliterationSettingsElementName);

            writer.WriteStartElement(ModeElementName);
            writer.WriteValue(Mode.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement(FileNameElementName);
            writer.WriteValue(FileName);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
