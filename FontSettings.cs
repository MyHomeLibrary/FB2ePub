using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using FontSettingsContracts;

namespace FontsSettings
{
    /// <summary>
    /// Set of settings relevant to font display
    /// </summary>
    public class EPubFontSettings : IEPubFontSettings, IXmlSerializable
    {
        private readonly List<ICSSFontFamily> _fontFamilies = new List<ICSSFontFamily>();
        private readonly List<ICSSStylableElement> _cssElements = new List<ICSSStylableElement>();


        #region ElementNames

        public const string FontsElementName = "Fonts";


        private const string FontFamiliesElementName = "FontFamilies";
        private const string CSSElementsElementName = "CSSElements";

        #endregion

        /// <summary>
        /// List of font families in setting
        /// </summary>
        public List<ICSSFontFamily> FontFamilies { get { return _fontFamilies; } }

        /// <summary>
        /// dictionary of CSS elements with their fonts assigned
        /// </summary>
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

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case FontFamiliesElementName:
                            ReadFonts(reader.ReadSubtree());
                            break;
                        case CSSElementsElementName:                           
                            ReadCSSElements(reader.ReadSubtree());
                            break;
                    }
                }
                reader.Read();
            } 
        }

        private void ReadCSSElements(XmlReader reader)
        {
            _cssElements.Clear();
            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case CSSStylableElement.CSSStylableElementName:
                            var cssElement = new CSSStylableElement();
                            cssElement.ReadXml(reader.ReadSubtree());
                            _cssElements.Add(cssElement);
                            break;
                    }
                }
                reader.Read();
            }           

        }

        private void ReadFonts(XmlReader reader)
        {
            _fontFamilies.Clear();
            while(!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case CSSFontFamily.FontFamilyElementName:

                            var fontFamily = new CSSFontFamily();
                            fontFamily.ReadXml(reader.ReadSubtree());
                            _fontFamilies.Add(fontFamily);
                            break;
                    }
                }
                reader.Read();
            } 
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(FontsElementName);
            WriteFontFamilies(writer);
            WriteCSSElements(writer);
            writer.WriteEndElement();
        }

        private void WriteCSSElements(XmlWriter writer)
        {
            writer.WriteStartElement(CSSElementsElementName);
            foreach (var cssStylableElement in _cssElements)
            {
                cssStylableElement.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        private void WriteFontFamilies(XmlWriter writer)
        {
            writer.WriteStartElement(FontFamiliesElementName);
            foreach (var cssFontFamily in _fontFamilies)
            {
                cssFontFamily.WriteXml(writer);
            }
            writer.WriteEndElement();
        }
    }
}
