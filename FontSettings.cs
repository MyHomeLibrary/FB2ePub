using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using FontSettingsContracts;

namespace FontsSettings
{
    /// <summary>
    /// Set of settings relevant to font display
    /// </summary>
    public class EPubFontSettings : IEPubFontSettings
    {
        private readonly List<ICSSFontFamily> _fontFamilies = new List<ICSSFontFamily>();
        private readonly List<ICSSStylableElement> _cssElements = new List<ICSSStylableElement>();

        /// <summary>
        /// List of font families in setting
        /// </summary>
        [XmlArray("FontFamilies"), XmlArrayItem(typeof(CSSFontFamily), ElementName = "FontFamily")]
        public List<ICSSFontFamily> FontFamilies { get { return _fontFamilies; } }

        /// <summary>
        /// dictionary of CSS elements with their fonts assigned
        /// </summary>
        [XmlArray("CSSElements"), XmlArrayItem(typeof(CSSStylableElement), ElementName = "CSSElement")]
        public List<ICSSStylableElement> CssElements { get { return _cssElements; } }


        public void CopyFrom(IEPubFontSettings ePubFontSettings)
        {
            if (ePubFontSettings == null)
            {
                throw new ArgumentNullException("ePubFontSettings");
            }
            _fontFamilies.Clear();
            foreach (var element in ePubFontSettings.FontFamilies)
            {
                _fontFamilies.Add(element);
            }

            _cssElements.Clear();
            foreach (var element in ePubFontSettings.CssElements)
            {
                _cssElements.Add(element);
            }

        }
    }
}
