using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace FontsSettings
{
    /// <summary>
    /// Types of font sources
    /// internal to this software
    /// 
    /// </summary>
    public enum DestinationTypeEnum
    {
        [XmlEnum(Name = "Local")]
        Local,
        [XmlEnum(Name = "External")]
        External,
        [XmlEnum(Name = "Embedded")]
        Embedded,
    }

    /// <summary>
    /// Another type of variation within a font family is the small-caps. 
    /// In a small-caps font the lower case letters look similar to the uppercase ones, but in a smaller size and with slightly different proportions. 
    /// The 'font-variant' property selects that font. 
    /// A value of 'normal' selects a font that is not a small-caps font, 'small-caps' selects a small-caps font. 
    /// It is acceptable (but not required) in CSS 2.1 if the small-caps font is a created by taking a normal font and replacing the lower case letters by scaled uppercase characters. 
    /// As a last resort, uppercase letters will be used as replacement for a small-caps font. 
    /// </summary>
    public enum FontVaiantEnum
    {
        [XmlEnum(Name = "")]
        None,

        [XmlEnum(Name = "normal")]
        Normal,

        [XmlEnum(Name = "small-caps")]
        SmallCaps,
    }

    /// <summary>
    /// The 'font-weight' property selects the weight of the font. 
    /// The values '100' to '900' form an ordered sequence, where each number indicates a weight that is at least as dark as its predecessor. 
    /// The keyword 'normal' is synonymous with '400', and 'bold' is synonymous with '700'. 
    /// Keywords other than 'normal' and 'bold' have been shown to be often confused with font names and a numerical scale was therefore chosen for the 9-value list. 
    /// </summary>
    public enum FontBoldnessEnum
    {
        [XmlEnum(Name = "")]
        None,

        [XmlEnum(Name = "100")]
        B100,

        [XmlEnum(Name = "200")]
        B200,

        [XmlEnum(Name = "300")]
        B300,

        [XmlEnum(Name = "400")]
        B400,

        [XmlEnum(Name = "normal")]
        Normal = B400,

        [XmlEnum(Name = "500")]
        B500,

        [XmlEnum(Name = "600")]
        B600,

        [XmlEnum(Name = "700")]
        B700,

        [XmlEnum(Name = "bold")]
        Bold = B700,

        [XmlEnum(Name = "800")]
        B800,

        [XmlEnum(Name = "900")]
        B900,

        [XmlEnum(Name = "lighter")]
        Lighter,

        [XmlEnum(Name = "bolder")]
        Bolder,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum GenericFamilyEnum
    {
        [XmlEnum(Name = "")]
        None,

        [XmlEnum(Name = "serif")]
        Serif,

        [XmlEnum(Name = "sans-serif")]
        SansSerif,

        [XmlEnum(Name = "cursive")]
        Cursive,

        [XmlEnum(Name = "fantasy")]
        Fantasy,

        [XmlEnum(Name = "monospace")]
        Monospace,
    }

    /// <summary>
    /// he 'font-style' property selects between normal (sometimes referred to as "roman" or "upright"), 
    /// italic and oblique faces within a font family. 
    /// </summary>
    public enum FontStylesEnum
    {
        [XmlEnum(Name = "")]
        None,

        [XmlEnum(Name = "normal")]
        Normal,

        [XmlEnum(Name = "italic")]
        Italic,

        [XmlEnum(Name = "oblique")]
        Oblique,
    }

    [XmlRoot(ElementName = "Destination", IsNullable = false)]
    public class Destination
    {
        /// <summary>
        /// Get/Set font path
        /// </summary>
        [XmlElement(ElementName = "Path")]
        public string Path { get; set;}

        /// <summary>
        /// Get/Set destination type
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
        public DestinationTypeEnum Type { get; set; }

    }

    [XmlRoot(ElementName = "Fonts", IsNullable = false)]
    public class  FontSettings : List<Font> 
    {
 
    }

    [Serializable]
    public class Font 
    {
        private readonly List<Destination> destinations = new List<Destination>();
        private readonly List<string> css_targets = new List<string>();
        
        /// <summary>
        /// Get list of destinations for the current font
        /// </summary>
        [XmlElement(ElementName = "Destinations", IsNullable = false)]
        public List<Destination> Destinations { get { return destinations;} }

        /// <summary>
        /// Get/Set generic family name , used as fallback in case family name font not found
        /// default: None 
        /// </summary>
        [XmlAttribute(AttributeName = "generic_family")]
        public GenericFamilyEnum GenericFamily { get; set; }

        ///// <summary>
        ///// Get/Set local path to take font from
        ///// </summary>
        //public string Path { get; set; }

        /// <summary>
        /// Get/Set font family name
        /// </summary>
        [XmlAttribute(AttributeName = "family_name")]
        public string FamilyName { get; set; }

        /// <summary>
        /// Get/Set if family name should be "decorated" with additional unique data to avoid Adobe "font cache bug"
        /// </summary>
        [XmlElement(ElementName = "decorate")]
        public bool DecorateFamilyName { get; set; }

        /// <summary>
        /// Get list of the CSS targets for current font
        /// </summary>
        [XmlElement(ElementName = "CSSTargets", IsNullable = false)]
        public List<string> CSSTargets { get { return css_targets; } }


        /// <summary>
        /// Get/Set font style
        /// </summary>
        [XmlAttribute(AttributeName = "style")]
        public FontStylesEnum Style { get; set; }

        /// <summary>
        /// Get/Set Font variant
        /// </summary>
        [XmlAttribute(AttributeName = "font-variant")]
        public FontVaiantEnum Variant { get; set; }

        /// <summary>
        /// Get/Set font weight (boldness)
        /// </summary>
        [XmlAttribute(AttributeName = "font-weight")]
        public FontBoldnessEnum Boldness { get; set; }

    }
}