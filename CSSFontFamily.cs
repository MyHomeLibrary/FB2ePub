using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using FontSettingsContracts;

namespace FontsSettings
{


    /// <summary>
    /// Represent set of fonts grouped into one family
    /// </summary>
    public class CSSFontFamily : ICSSFontFamily
    {
        private readonly List<ICSSFont> _fonts = new List<ICSSFont>();
        private string _name = GenerateUniqueName();

        #region constants

        public const string FontFamilyElementName = "FontFamily";
        private const string NameAttributeName = "name";

        #endregion 

        /// <summary>
        /// Generates unique name for the font family
        /// </summary>
        /// <returns></returns>
        private static string GenerateUniqueName()
        {
            return string.Format("Fonts_{0}",Guid.NewGuid().ToString("D"));
        }

        /// <summary>
        /// List of fonts belong to the family
        /// </summary>
        public List<ICSSFont> Fonts { get { return _fonts; } }

        /// <summary>
        /// Name of the family
        /// </summary>
        public string Name
        {
            get
            {
                return MakeDecoratedName(_name,DecorationId);
            }
            set { if (!string.IsNullOrEmpty(value)) _name = value; }
        }


        public static string MakeDecoratedName(string baseName,string decoration)
        {
            if (!string.IsNullOrEmpty(decoration))
            {
                return string.Format("{0}_{1}", baseName, decoration);
            }
            return baseName;
        }

        /// <summary>
        /// if not empty than resulting family name 'decorated' by this value
        /// </summary>
        [XmlIgnore]
        public string DecorationId { get; set; }

        /// <summary>
        /// Copy from another CSSFontFamily allocating new objects
        /// </summary>
        /// <param name="cssFontFamily"></param>
        public void CopyFrom(ICSSFontFamily cssFontFamily)
        {
            if (cssFontFamily == null)
            {
                throw new ArgumentNullException("cssFontFamily");
            }
            if (cssFontFamily == this)
            {
                return;
            }
            _name = cssFontFamily.Name;
            _fonts.Clear();
            foreach (var cssFont in cssFontFamily.Fonts)
            {
                CSSFont newFont = new CSSFont();
                newFont.CopyFrom(cssFont);
                _fonts.Add(newFont);
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _fonts.Clear();
            if (!reader.ReadAttributeValue())
            {
                throw new InvalidDataException("Unable to read attributes from Font family node");
            }
            string nameAttribute = reader.GetAttribute(NameAttributeName);
            if (string.IsNullOrEmpty(nameAttribute))
            {
                throw new InvalidDataException("Font family name can't be empty");
            }
            _name = nameAttribute;
            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case  CSSFont.FontElementName:
                            var font = (CSSFont)reader.ReadElementContentAs(typeof(CSSFont), null);
                            _fonts.Add(font);
                            continue;
                    }
                }
                reader.Read();
            } 

        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(FontFamilyElementName);
            writer.WriteAttributeString(NameAttributeName,_name);
            foreach (var cssFont in _fonts)
            {
                cssFont.WriteXml(writer);
            }
            writer.WriteEndElement();
        }
    }
}
