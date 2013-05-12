using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FontsSettings
{
    /// <summary>
    /// Represent any element (HTTP or .class) that can be styled
    /// </summary>
    /// 
    public class CSSStylableElement
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

        internal void CopyFrom(CSSStylableElement element)
        {
            _name= element._name;
            _class= element._class;
            _assignedFonts.Clear();
            foreach (var font in element._assignedFonts)
            {
                _assignedFonts.Add(font);
            }
        }
    }
}
