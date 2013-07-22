using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FontsSettings;

namespace EPubLibrary.CSS_Items
{
    public class CssFontDefinition : BaseCSSItem
    {
        public CssFontDefinition()
        {
            Name = "@font-face";
        }

        public string Family
        {
            get { if (parameters.ContainsKey("font-family"))
                    return parameters["font-family"].ToString();
                return string.Empty;}
            set { if (!string.IsNullOrEmpty(value)) parameters["font-family"] = value; }
        }

        public string FontStyle
        {
            get { if (parameters.ContainsKey("font-style"))
                    return parameters["font-style"].ToString();
                return string.Empty;
            }
            set { if (!string.IsNullOrEmpty(value)) parameters["font-style"] = value.ToLower(); }           
        }

        public string FontWidth
        {
            get { if (parameters.ContainsKey("font-weight"))
                    return parameters["font-weight"].ToString();
                return "normal";
            }
            set { if (!string.IsNullOrEmpty(value)) parameters["font-weight"] = value; }                       
        }


        public List<string> FontSrcs
        {
            get
            {
                if (parameters.ContainsKey("src"))
                    return parameters["src"] as List<string>;
                return new List<string>();
            }
            set { if (value != null) parameters["src"] = value; }
        }

        public override bool Equals(object obj)
        {
            CssFontDefinition otherFont = obj as CssFontDefinition;
            if (otherFont == null)
            {
                return false;
            }
            if (Family != otherFont.Family)
            {
                return false;
            }
            if (FontStyle != otherFont.FontStyle)
            {
                return false;
            }
            if (FontWidth != otherFont.FontWidth)
            {
                return false;
            }
            if (FontSrcs != otherFont.FontSrcs)
            {
                if (!FontSrcs.SequenceEqual(otherFont.FontSrcs))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Convert from font style enumiration to string to be used in CSS
        /// </summary>
        /// <param name="style">style to convert</param>
        /// <returns>resulting style as string</returns>
        public static string FromStyle(FontStylesEnum style)
        {
            switch (style)
            {
                case FontStylesEnum.Normal:
                    return "normal";
                case FontStylesEnum.Italic:
                    return "italic";
                case FontStylesEnum.Oblique:
                    return "oblique";
            }
            return string.Empty;
        }

        /// <summary>
        /// Convert from the font boldness enumiration to string to be used in CSS
        /// </summary>
        /// <param name="fontBoldnessEnum">boldness to convert</param>
        /// <returns>resulting width as string</returns>
        public static string FromWidth(FontBoldnessEnum fontBoldnessEnum)
        {
            switch (fontBoldnessEnum)
            {
                case FontBoldnessEnum.B100:
                    return "100";
                case FontBoldnessEnum.B200:
                    return "200";
                case FontBoldnessEnum.B300:
                    return "300";
                case FontBoldnessEnum.B400:
                    return "normal";
                case FontBoldnessEnum.B500:
                    return "500";
                case FontBoldnessEnum.B600:
                    return "600";
                case FontBoldnessEnum.B700:
                    return "bold";
                case FontBoldnessEnum.B800:
                    return "800";
                case FontBoldnessEnum.B900:
                    return "900";
                case FontBoldnessEnum.Lighter:
                    return "lighter";
                case FontBoldnessEnum.Bolder:
                    return "bolder";
            }
            return string.Empty;
        }


        /// <summary>
        /// Convert source objects into a source string
        /// </summary>
        /// <param name="fontSource"></param>
        /// <param name="embedStyles"></param>
        /// <param name="flatStructure"></param>
        /// <returns></returns>
        public static string ConvertToSourceString(FontSource fontSource, bool embedStyles,bool flatStructure)
        {
            StringBuilder builder = new StringBuilder();
                switch (fontSource.Type)
                {
                    case SourceTypes.External:
                        builder.AppendFormat(@" url({0}) ", fontSource.Location);
                        break;
                    case SourceTypes.Local:
                        builder.AppendFormat(" local(\"{0}\") ", fontSource.Location);
                        break;
                    case SourceTypes.Embedded:
                        if (!embedStyles)
                        {
                            builder.AppendFormat(flatStructure ? @" url({0}) " : @" url(../fonts/{0}) ", Path.GetFileName(fontSource.Location.ToLower()));
                        }
                        else
                        {
                            builder.AppendFormat(flatStructure ? @" url(../{0}) " : @" url(fonts/{0}) ", Path.GetFileName(fontSource.Location.ToLower()));
                        }
                        break;
                    default:
                        Logger.Log.ErrorFormat("Unknown font source type : {0}", fontSource.Type);
                        break;
            }
            return builder.ToString();

        }
    }
}