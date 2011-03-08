using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPubLibrary.CSS_Items
{
    public class FontDefinition : BaseCSSItem
    {
        public FontDefinition()
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

        public  string FontSrc
        {
            get { if (parameters.ContainsKey("src"))
                    return parameters["src"].ToString();
                return string.Empty;
            }
            set { if (!string.IsNullOrEmpty(value)) parameters["src"] = value; }                                   
        }

        public override bool Equals(object obj)
        {
            FontDefinition otherFont = obj as FontDefinition;
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
            if (FontSrc != otherFont.FontSrc)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}