using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ConverterContracts.FontSettings;

namespace FontsSettings
{
    /// <summary>
    /// Represent one source of the font
    /// </summary>
    [Serializable]
    public class FontSource : IFontSource
    {
        /// <summary>
        /// Type of the source
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
        public SourceTypes Type { get; set; }

        /// <summary>
        /// Location of the source file or in case of local name of the file
        /// </summary>
        [XmlElement(ElementName = "Location")]
        public string Location { get; set; }

        /// <summary>
        /// Format of the font file
        /// </summary>
        [XmlAttribute(AttributeName = "format")]
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
    }
}
