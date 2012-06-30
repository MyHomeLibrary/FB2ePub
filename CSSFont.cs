﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FontsSettings
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
    public  enum FontStretch
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

    /// <summary>
    /// Represent one font 
    /// </summary>
    [Serializable]
    public class CSSFont
    {
        private FontBoldnessEnum _width = FontBoldnessEnum.Normal;

        private readonly List<FontSource> _sources = new List<FontSource>();

        [XmlIgnore]
        public bool HasSources { get { return (_sources.Count != 0); } }

        [XmlIgnore]
        public string Name
        {
            get
            {
                if (Sources.Count == 0)
                {
                    return "Undefined font";
                }
                StringBuilder sb = new StringBuilder();
                foreach (var fontSource in Sources)
                {
                    string fontName = GetFontName(fontSource);
                    if (sb.Length == 0)
                    {
                        sb.Append("Font ");
                        sb.Append(fontName);
                    }
                    else
                    {
                        sb.AppendFormat(" , {0}", fontName);
                    }
                }
                sb.AppendFormat(" (width=\"{0}\" , style=\"{1}\", variant=\"{2}\", stretch=\"{3}\")", GetFontWidth(), GetFontStyle(), GetFontVariant(),
                                GetFontStretch());
                return sb.ToString();
            }
        }


        private string GetFontStretch()
        {
            string result = string.Empty;
            switch (FontStretch)
            {
                case FontStretch.Condenced:
                    result = "condensed";
                    break;
                case FontStretch.Expanded:
                    result = "expanded";
                    break;
                case FontStretch.ExtraCondenced:
                    result = "extra-condensed";
                    break;
                case FontStretch.ExtraExpanded:
                    result = "extra-expanded";
                    break;
                case FontStretch.Normal:
                    result = "normal";
                    break;
                case FontStretch.SemiCondenced:
                    result = "semi-condensed";
                    break;
                case FontStretch.SemiExpanded:
                    result = "ultra-expanded";
                    break;
                case FontStretch.UltraCondenced:
                    result = "ultra-condensed";
                    break;
                case FontStretch.UltraExpanded:
                    result = "semi-expanded";
                    break;
                default:
                    result = "?";
                    break;
            }
            return result;
        }

        private string GetFontVariant()
        {
            string result = string.Empty;
            switch (FontVariant)
            {
                case FontVaiantEnum.Normal:
                    result = "normal";
                    break;
                case FontVaiantEnum.SmallCaps:
                    result = "small-caps";
                    break;
                default:
                    result = "?";
                    break;
            }
            return result;
        }

        private string GetFontStyle()
        {
            string result = string.Empty;
            switch (FontStyle)
            {
                case FontStylesEnum.Normal:
                    result = "normal";
                    break;
                case FontStylesEnum.Italic:
                    result = "italic";
                    break;
                case FontStylesEnum.Oblique:
                    result = "oblique";
                    break;
                default:
                    result = "?";
                    break;

            }
            return result;
        }

        private string GetFontWidth()
        {
            string result = string.Empty;
            switch (FontWidth)
            {
                case FontBoldnessEnum.Normal: // same as B400
                    result = "normal";
                    break;
                case FontBoldnessEnum.Bold: // same as B700
                    result = "bold";
                    break;
                case FontBoldnessEnum.Bolder:
                    result = "bolder";
                    break;
                case FontBoldnessEnum.Lighter:
                    result = "lighter";
                    break;
                case FontBoldnessEnum.B100:
                    result = "100";
                    break;
                case FontBoldnessEnum.B200:
                    result = "200";
                    break;
                case FontBoldnessEnum.B300:
                    result = "300";
                    break;
                case FontBoldnessEnum.B500:
                    result = "500";
                    break;
                case FontBoldnessEnum.B600:
                    result = "600";
                    break;
                case FontBoldnessEnum.B800:
                    result = "800";
                    break;
                case FontBoldnessEnum.B900:
                    result = "900";
                    break;
                default:
                    result = "?";
                    break;
            }
            return result;
        }

        private static string GetFontName(FontSource fontSource)
        {
            switch (fontSource.Type)
            {
                case SourceTypes.Local:
                    return fontSource.Location;
                case SourceTypes.External:
                case SourceTypes.Embedded:
                    string shortName = Path.GetFileNameWithoutExtension(fontSource.Location);
                    if (!string.IsNullOrEmpty(shortName))
                    {
                        return shortName;
                    }
                    return fontSource.Location;
            }
            return string.Empty;
        }

        /// <summary>
        /// Style of the font in question
        /// </summary>
        [XmlAttribute(AttributeName = "style")]
        public FontStylesEnum FontStyle { get; set; }

        /// <summary>
        /// Variant of the font
        /// </summary>
        [XmlAttribute(AttributeName = "variant")]
        public FontVaiantEnum FontVariant { get; set; }


        /// <summary>
        /// Width (boldness) of the font
        /// </summary>
        [XmlAttribute(AttributeName = "width")]
        public FontBoldnessEnum FontWidth 
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Font stretch
        /// </summary>
        [XmlAttribute(AttributeName = "stretch")]
        public FontStretch FontStretch { get; set; }

        /// <summary>
        /// List of the font sources
        /// </summary>
        [XmlElement(ElementName = "Source")]
        public List<FontSource> Sources { get { return _sources; } }

        public void CopyFrom(CSSFont cssFont)
        {
            _width = cssFont._width;
            FontStyle = cssFont.FontStyle;
            FontVariant = cssFont.FontVariant;
            FontStretch = cssFont.FontStretch;
            _sources.Clear();
            foreach (var fontSource in cssFont._sources)
            {
                FontSource newSource = new FontSource();
                newSource.CopyFrom(fontSource);
                _sources.Add(newSource);
            }
        }

    }
}
