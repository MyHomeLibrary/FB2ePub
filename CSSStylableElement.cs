using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
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

        #region constants

        public const string CSSStylableElementName = "CSSElement";

        private const string NameAttributeName = "name";
        private const string ClassAttributeName = "class";

        private const string FontElementName = "Font";

        #endregion

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            _name = reader.GetAttribute(NameAttributeName);

            _class = reader.GetAttribute(ClassAttributeName);

            while (!reader.EOF)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case FontElementName:
                            _assignedFonts.Add(reader.ReadElementContentAsString());
                            continue;
                    }
                }
                reader.Read();
            } 

            
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(CSSStylableElementName);
            writer.WriteAttributeString(NameAttributeName,_name);
            writer.WriteAttributeString(ClassAttributeName,_class);
            foreach (var assignedFont in _assignedFonts)
            {
                writer.WriteStartElement(FontElementName);
                writer.WriteValue(assignedFont);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
