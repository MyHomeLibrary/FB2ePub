using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using ConverterContracts.FontSettings;

namespace FontsSettings
{  
    

    /// <summary>
    /// Represent one font 
    /// </summary>
    [Serializable]
    public class CSSFont : ICSSFont
    {
        private FontBoldnessEnum _width = FontBoldnessEnum.Normal;

        private readonly List<IFontSource> _sources = new List<IFontSource>();

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
            string result;
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
            string result;
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
            string result;
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
            string result;
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

        private static string GetFontName(IFontSource fontSource)
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
        public List<IFontSource> Sources { get { return _sources; } }

        public void CopyFrom(ICSSFont cssFont)
        {
            if (cssFont == null)
            {
                throw new ArgumentNullException("cssFont");
            }
            if (cssFont == this)
            {
                return;
            }
            _width = cssFont.FontWidth;
            FontStyle = cssFont.FontStyle;
            FontVariant = cssFont.FontVariant;
            FontStretch = cssFont.FontStretch;
            _sources.Clear();
            foreach (var fontSource in cssFont.Sources)
            {
                var newSource = new FontSource();
                newSource.CopyFrom(fontSource);
                _sources.Add(newSource);
            }
        }

    }
}
