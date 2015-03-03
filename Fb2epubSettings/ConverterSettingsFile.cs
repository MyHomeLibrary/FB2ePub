using System.IO;
using System.Xml.Serialization;
using ConverterContracts.Settings;

namespace Fb2epubSettings
{
    /// <summary>
    /// Used to serialize converter settings to/from file
    /// </summary>
    public class ConverterSettingsFile
    {
        private readonly IConverterSettings _settings = new ConverterSettings();
        private XmlSerializer _serializer;


        public IConverterSettings Settings { get { return _settings; } }

        /// <summary>
        /// Load converter settings from file
        /// </summary>
        /// <param name="fileName">name of the file to load</param>
        public void Load(string fileName)
        {
            if (_serializer == null)
            {
                _serializer = new XmlSerializer(_settings.GetType());
            }
            using (FileStream fs = File.OpenRead(fileName))
            {
                ConverterSettings temp = _serializer.Deserialize(fs) as ConverterSettings;
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
            if (_serializer == null)
            {
                _serializer = new XmlSerializer(_settings.GetType());
            }
            using (FileStream fs = File.Create(fileName))
            {
                _serializer.Serialize(fs, _settings);
            }
        }
    }
}
