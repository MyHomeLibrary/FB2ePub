using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ConverterContracts.FontSettings;

namespace FontsSettings
{


    /// <summary>
    /// Represent set of fonts grouped into one family
    /// </summary>
    [Serializable]
    public class CSSFontFamily : ICSSFontFamily
    {
        private readonly List<ICSSFont> _fonts = new List<ICSSFont>();
        private string _name = GenerateUniqueName();

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
        [XmlElement(ElementName = "Font")]
        public List<ICSSFont> Fonts { get { return _fonts; } }

        /// <summary>
        /// Name of the family
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
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
    }
}
