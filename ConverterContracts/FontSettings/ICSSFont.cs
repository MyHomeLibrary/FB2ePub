using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConverterContracts.FontSettings
{
    #region Font_enums
    /// <summary>
    /// The 'font-style' property selects between normal (sometimes referred to as "roman" or "upright"), 
    /// italic and oblique faces within a font family. 
    /// </summary>
    public enum FontStylesEnum
    {
        [XmlEnum(Name = "normal")]
        Normal = 0,

        [XmlEnum(Name = "italic")]
        Italic,

        [XmlEnum(Name = "oblique")]
        Oblique,
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
        [XmlEnum(Name = "normal")]
        Normal = 0,

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
    /// Font stretch settings
    /// the ‘font-stretch’ property selects a normal, condensed, or expanded face from a font family.
    /// </summary>
    public enum FontStretch
    {
        [XmlEnum(Name = "normal")]
        Normal = 0,

        [XmlEnum(Name = "ultra-condensed")]
        UltraCondenced,

        [XmlEnum(Name = "extra-condensed")]
        ExtraCondenced,

        [XmlEnum(Name = "condensed")]
        Condenced,

        [XmlEnum(Name = "semi-condensed")]
        SemiCondenced,

        [XmlEnum(Name = "semi-expanded")]
        SemiExpanded,

        [XmlEnum(Name = "expanded")]
        Expanded,

        [XmlEnum(Name = "extra-expanded")]
        ExtraExpanded,

        [XmlEnum(Name = "ultra-expanded")]
        UltraExpanded
    }

    #endregion

    public interface ICSSFont
    {
        bool HasSources { get; }
        string Name { get; }
        FontStylesEnum FontStyle { get; set; }
        FontVaiantEnum FontVariant { get; set; }
        FontBoldnessEnum FontWidth { get; set; }
        FontStretch FontStretch { get; set; }
        List<IFontSource> Sources { get; }

        void CopyFrom(ICSSFont cssFont);
    }
}
