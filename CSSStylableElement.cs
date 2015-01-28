using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using FontSettingsContracts;

namespace FontsSettings
{
    /// <summary>
    /// Represent any element (HTTP or .class) that can be styled
    /// </summary>
    /// 
    public class CSSStylableElement : ICSSStylableElement
    {
        private string _name = string.Empty;
        private string _class = string.Empty;
        private readonly List<string> _assignedFonts = new List<string>();

        [XmlAttribute(AttributeName= "name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [XmlAttribute(AttributeName = "class")]
        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        [XmlElement(ElementName = "Font")]
        public List<string> AssignedFontFamilies
        {
            get { return _assignedFonts; }
        }

        public void CopyFrom(ICSSStylableElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (element == this)
            {
                return;
            }
            _name= element.Name;
            _class= element.Class;
            _assignedFonts.Clear();
            foreach (var font in element.AssignedFontFamilies)
            {
                _assignedFonts.Add(font);
            }
        }
    }
}
