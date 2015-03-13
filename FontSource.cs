using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using FontSettingsContracts;

namespace FontsSettings
{
    /// <summary>
    /// Represent one source of the font
    /// </summary>
    public class FontSource : IFontSource
    {

        #region constants

        private const string TypeAttributeName = "type";
        private const string FormatAttributeName = "format";

        private const string LocationElementName = "Location";
        public const string SourceElementName = "Source";

        #endregion
        /// <summary>
        /// Type of the source
        /// </summary>
        public SourceTypes Type { get; set; }

        /// <summary>
        /// Location of the source file or in case of local name of the file
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Format of the font file
        /// </summary>
        public FontFormat Format { set; get; }

        public void CopyFrom(IFontSource fontSource)
        {
            if (fontSource == null)
            {
                throw new ArgumentNullException("fontSource");
            }
            if (fontSource == this)
            {
                return;
            }
            Type = fontSource.Type;
            Location = fontSource.Location;
            Format = fontSource.Format;
        }

        [XmlIgnore]
        public bool EmbeddedLocation
        {
            get
            {
                return Type == SourceTypes.Embedded;
            }
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(Location))
                {
                    return "Undefined source";
                }
                return Location;
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            string typeAttribute = reader.GetAttribute(TypeAttributeName);
            if (string.IsNullOrEmpty(typeAttribute))
            {
                throw new InvalidDataException("Type attribute can't be empty");
            }
            SourceTypes type;
            if (!Enum.TryParse(typeAttribute, true, out type))
            {
                throw new InvalidDataException(string.Format("Invalid type value: {0}", typeAttribute));
            }
            Type = type;

            string formatAttribute = reader.GetAttribute(FormatAttributeName);
            if (string.IsNullOrEmpty(formatAttribute))
            {
                throw new InvalidDataException("Format attribute can't be empty");
            }
            FontFormat format;
            if (!Enum.TryParse(formatAttribute, true, out format))
            {
                throw new InvalidDataException(string.Format("Invalid format value: {0}", formatAttribute));
            }
            Format = format;

            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case LocationElementName:
                            Location = reader.ReadElementContentAsString();
                            continue;
                    }
                }
                reader.Read();
            } 
 
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(SourceElementName);
            writer.WriteAttributeString(TypeAttributeName,Type.ToString());
            writer.WriteAttributeString(FormatAttributeName, Format.ToString());
            writer.WriteStartElement(LocationElementName);
            writer.WriteValue(Location);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}
