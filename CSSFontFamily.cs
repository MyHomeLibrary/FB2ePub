using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FontsSettings
{


    /// <summary>
    /// Represent set of fonts grouped into one family
    /// </summary>
    [Serializable]
    public class CSSFontFamily
    {
        private readonly List<CSSFont> _fonts = new List<CSSFont>();
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
        public List<CSSFont> Fonts { get { return _fonts; } }

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
        public string DecorationId { get; set; }

        /// <summary>
        /// Copy from another CSSFontFamily allocating new objects
        /// </summary>
        /// <param name="cssFontFamily"></param>
        public void CopyFrom(CSSFontFamily cssFontFamily)
        {
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
