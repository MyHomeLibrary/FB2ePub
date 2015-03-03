using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using FontSettingsContracts;

namespace FontsSettings
{  
    

    /// <summary>
    /// Represent one font 
    /// </summary>
    public class CSSFont : ICSSFont
    {
        private FontBoldnessEnum _width = FontBoldnessEnum.Normal;

        private readonly List<IFontSource> _sources = new List<IFontSource>();


        #region constants

        public const string FontElementName = "Font";
        private const string StyleAttributeName = "style";
        private const string VariantAttributeName = "variant";
        private const string WidthAttributeName = "width";
        private const string StretchAttributeName = "stretch";
        #endregion


        public bool HasSources { get { return (_sources.Count != 0); } }

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


        /// <summary>
        /// Style of the font in question
        /// </summary>
        public FontStylesEnum FontStyle { get; set; }

        /// <summary>
        /// Variant of the font
        /// </summary>
        public FontVaiantEnum FontVariant { get; set; }


        /// <summary>
        /// Width (boldness) of the font
        /// </summary>
        public FontBoldnessEnum FontWidth 
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Font stretch
        /// </summary>
        public FontStretch FontStretch { get; set; }

        /// <summary>
        /// List of the font sources
        /// </summary>
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.ReadAttributeValue())
            {
                throw new InvalidDataException("Unable to read attributes from Font family node");
            }
            string styleAttribute = reader.GetAttribute(StyleAttributeName);
            if (string.IsNullOrEmpty(styleAttribute))
            {
                throw new InvalidDataException("Style attribute can't be empty");
            }
            FontStylesEnum style;
            if (!Enum.TryParse(styleAttribute, true, out style))
            {
                throw new InvalidDataException(string.Format("Invalid style value: {0}",styleAttribute));
            }
            FontStyle = style;


            string variantAttribute = reader.GetAttribute(VariantAttributeName);
            if (string.IsNullOrEmpty(variantAttribute))
            {
                throw new InvalidDataException("Variant attribute can't be empty");
            }
            FontVaiantEnum variant;
            if (!Enum.TryParse(variantAttribute, true, out variant))
            {
                throw new InvalidDataException(string.Format("Invalid variant value: {0}", variantAttribute));
            }
            FontVariant = variant;

            string widthAttribute = reader.GetAttribute(WidthAttributeName);
            if (string.IsNullOrEmpty(widthAttribute))
            {
                throw new InvalidDataException("Width attribute can't be empty");
            }
            FontBoldnessEnum width;
            if (!Enum.TryParse(widthAttribute, true, out width))
            {
                throw new InvalidDataException(string.Format("Invalid Width value: {0}", widthAttribute));
            }
            FontWidth = width;

            string stretchAttribute = reader.GetAttribute(StretchAttributeName);
            if (string.IsNullOrEmpty(stretchAttribute))
            {
                throw new InvalidDataException("Stretch attribute can't be empty");
            }
            FontStretch stretch;
            if (!Enum.TryParse(stretchAttribute, true, out stretch))
            {
                throw new InvalidDataException(string.Format("Invalid Stretch value: {0}", stretchAttribute));
            }
            FontStretch = stretch;


            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case FontSource.SourceElementName:
                            var source = (FontSource) reader.ReadElementContentAs(typeof (FontSource), null);
                            _sources.Add(source);
                            continue;
                    }
                }
                reader.Read();
            } 

        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(FontElementName);
            writer.WriteAttributeString(StyleAttributeName,FontStyle.ToString());
            writer.WriteAttributeString(VariantAttributeName,FontVariant.ToString());
            writer.WriteAttributeString(WidthAttributeName,FontWidth.ToString());
            writer.WriteAttributeString(StretchAttributeName, FontStretch.ToString());
            foreach (var fontSource in _sources)
            {
                fontSource.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        #region private_functions for bulilding name
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

        #endregion 

    }
}
