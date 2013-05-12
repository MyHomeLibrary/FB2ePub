using FontsSettings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fb2epubSettings
{
    /// <summary>
    /// Used to serialize converter settings to/from file
    /// </summary>
    public class ConverterSettingsFile
    {
        protected readonly ConverterSettings _settings = new ConverterSettings();


        public ConverterSettings Settings { get { return _settings; } }

        /// <summary>
        /// Load converter settings from file
        /// </summary>
        /// <param name="fileName">name of the file to load</param>
        public void Load(string fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(_settings.GetType(), new Type[] { typeof(EPubFontSettings), typeof(CSSFontFamily), typeof(CSSStylableElement) });
                ConverterSettings temp   =   serializer.Deserialize(fs) as ConverterSettings;
                if (temp != null)
                {
                    _settings.CopyFrom(temp);
                }
            }
        }

        /// <summary>
        /// Saves converter settings to file
        /// </summary>
        /// <param name="fileName">name of the file to save to</param>
        public void Save(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(_settings.GetType(), new Type[] { typeof(EPubFontSettings), typeof(CSSFontFamily), typeof(CSSStylableElement) });
            using (FileStream fs = File.Create(fileName))
            {
                serializer.Serialize(fs, _settings);
            }
        }
    }
}
