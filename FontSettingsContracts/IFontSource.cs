using System.Xml.Serialization;

namespace FontSettingsContracts
{
    /// <summary>
    /// Possible font source types
    /// </summary>
    public enum SourceTypes
    {
        Embedded = 0, // embeded, same as external but the font file will be added to resulting ePub
        External, // external - contains link to the font file or font file url
        Local // name of the file local to reader device
    }

    /// <summary>
    /// Format of the font file
    /// </summary>
    public enum FontFormat
    {
        Unknown = 0,
        WOFF,
        TrueType,
        OpenType,
        EmbeddedOpenType,
        SVGFont,
    }


    public interface IFontSource : IXmlSerializable
    {
        SourceTypes Type { get; set; }
        string Location { get; set; }
        FontFormat Format { set; get; }
        bool EmbeddedLocation { get; }
        string Name { get; }

        void CopyFrom(IFontSource fontSource);
    }
}
