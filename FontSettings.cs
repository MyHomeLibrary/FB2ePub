using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FontsSettings
{
    /// <summary>
    /// Set of settings relevant to font display
    /// </summary>
    [XmlRoot(ElementName = "FontSettings", IsNullable = false)]
    public class EPubFontSettings
    {
        private readonly List<CSSFontFamily> _fontFamilies = new List<CSSFontFamily>();
        private readonly List<CSSStylableElement> _cssElements = new List<CSSStylableElement>();

        /// <summary>
        /// List of font families in setting
        /// </summary>
        [XmlElement(ElementName = "FontFamily")]
        public List<CSSFontFamily> FontFamilies { get { return _fontFamilies; } }

        /// <summary>
        /// dictionary of CSS elements with their fonts assigned
        /// </summary>
        [XmlElement(ElementName = "CSSElement")]
        public List<CSSStylableElement> CssElements { get { return _cssElements; } }

    }
}
