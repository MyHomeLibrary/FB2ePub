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
    public class EPubFontSettings
    {
        private readonly List<CSSFontFamily> _fontFamilies = new List<CSSFontFamily>();
        private readonly List<CSSStylableElement> _cssElements = new List<CSSStylableElement>();

        /// <summary>
        /// List of font families in setting
        /// </summary>
        [XmlArray("FontFamilies"), XmlArrayItem(typeof(CSSFontFamily), ElementName = "FontFamily")]
        public List<CSSFontFamily> FontFamilies { get { return _fontFamilies; } }

        /// <summary>
        /// dictionary of CSS elements with their fonts assigned
        /// </summary>
        [XmlArray("CSSElements"), XmlArrayItem(typeof(CSSStylableElement), ElementName = "CSSElement")]
        public List<CSSStylableElement> CssElements { get { return _cssElements; } }


        public void CopyFrom(EPubFontSettings ePubFontSettings)
        {
            if (ePubFontSettings == null)
            {
                throw new ArgumentNullException("ePubFontSettings");
            }
            _fontFamilies.Clear();
            foreach (var element in ePubFontSettings._fontFamilies)
            {
                _fontFamilies.Add(element);
            }

            _cssElements.Clear();
            foreach (var element in ePubFontSettings._cssElements)
            {
                _cssElements.Add(element);
            }

        }
    }
}
