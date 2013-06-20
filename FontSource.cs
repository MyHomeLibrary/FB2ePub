using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FontsSettings
{
    /// <summary>
    /// Possible font source types
    /// </summary>
    public enum SourceTypes
    {
        [XmlEnum(Name = "Embedded")]
        Embedded = 0, // embeded, same as external but the font file will be added to resulting ePub

        [XmlEnum(Name = "External")]
        External, // external - contains link to the font file or font file url

        [XmlEnum(Name = "Local")]
        Local // name of the file local to reader device
    }

    /// <summary>
    /// Format of the font file
    /// </summary>
    public enum FontFormat
    {
        [XmlEnum(Name = "")]
        Unknown= 0,

        [XmlEnum(Name = "woff")]
        WOFF,

        [XmlEnum(Name = "truetype")]
        TrueType,

        [XmlEnum(Name = "opentype")]
        OpenType,

        [XmlEnum(Name = "embedded-opentype")]
        EmbeddedOpenType,

        [XmlEnum(Name = "svg")]
        SVGFont,
    }

    /// <summary>
    /// Represent one source of the font
    /// </summary>
    [Serializable] 
    public class FontSource
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

        internal void CopyFrom(FontSource fontSource)
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
