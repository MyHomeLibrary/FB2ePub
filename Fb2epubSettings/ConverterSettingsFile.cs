﻿using System.Collections;
using Fb2epubSettings.AppleSettings;
using Fb2epubSettings.AppleSettings.ePub_v2;
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
        //private Hashtable _serializers = new Hashtable();
        private XmlSerializer _serializer = null;


        public ConverterSettings Settings { get { return _settings; } }

        /// <summary>
        /// Load converter settings from file
        /// </summary>
        /// <param name="fileName">name of the file to load</param>
        public void Load(string fileName)
        {
            if (_serializer == null)
            {
                _serializer = new XmlSerializer(_settings.GetType(), GetSerializerTypes());
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
                _serializer = new XmlSerializer(_settings.GetType(), GetSerializerTypes());
            }
            using (FileStream fs = File.Create(fileName))
            {
                _serializer.Serialize(fs, _settings);
            }
        }

        private Type[] GetSerializerTypes()
        {
            return new[] {typeof (EPubFontSettings),
                    typeof (CSSFontFamily),
                    typeof (CSSStylableElement),
                    typeof (AppleConverterePub2Settings),
                    typeof (AppleEPub2PlatformSettings)};
        }
    }
}
