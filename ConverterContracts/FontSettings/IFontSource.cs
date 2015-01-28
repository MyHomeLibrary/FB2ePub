using System.Xml.Serialization;

namespace ConverterContracts.FontSettings
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
        Unknown = 0,

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


    public interface IFontSource
    {
        SourceTypes Type { get; set; }
        string Location { get; set; }
        FontFormat Format { set; get; }
        bool EmbeddedLocation { get; }
        string Name { get; }

        void CopyFrom(IFontSource fontSource);
    }
}
