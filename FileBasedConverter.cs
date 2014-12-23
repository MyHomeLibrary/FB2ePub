using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace TranslitRu
{
    internal  class FileBasedConverter : BaseTrasLitConverter
    {

        private readonly Dictionary<char,string> _ruleTable = new Dictionary<char, string>();
        private string _fileName;

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                _ruleTable.Clear();
            }
        }


        public override string ConvertCharacter(char inputCharacter)
        {
            if (_ruleTable.Count == 0)
            {
                LoadRuleFile();
            }
            // temporary
            if (_ruleTable.ContainsKey(inputCharacter))
            {
                return _ruleTable[inputCharacter];
            }
            return inputCharacter.ToString(CultureInfo.InvariantCulture);
        }

        private void LoadRuleFile()
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                throw new NullReferenceException("Please set file name first");
            }
            if (!File.Exists(_fileName))
            {
                throw new Exception(string.Format("File {0} does not exists on disk",Path.GetFullPath(_fileName)));
            }

            using (Stream stream = File.OpenRead(_fileName))
            {
                var settings = new XmlReaderSettings
                {
                    ValidationType = ValidationType.Schema,
                };
                using (XmlReader reader = XmlReader.Create(stream, settings))
                {
                    XDocument fileDocument = XDocument.Load(reader, LoadOptions.PreserveWhitespace);
                    LoadRulesData(fileDocument);
                    reader.Close();
                }
                stream.Close();
            }
        }


        private void LoadRulesData(XDocument fileDocument)
        {
            if (fileDocument == null)
            {
                throw new ArgumentNullException("fileDocument");
            }
            if (fileDocument.Root == null)
            {
                throw new NullReferenceException("Document root can't be empty");
            }
            if (fileDocument.Root.Name.LocalName != "transtable")
            {
                throw new Exception("invalid file format the root should be 'transtable'");
            }

            XNamespace nameSpace = "http://www.lordkiron.com/translit";

            _ruleTable.Clear();
            foreach (var element in fileDocument.Root.Elements(nameSpace + "map"))
            {
                XAttribute xin = element.Attribute("in");
                if (xin == null || string.IsNullOrEmpty(xin.Value))
                {
                    Debug.Fail(string.Format("Invalid file format, 'in' attribute is required"));
                    continue;
                }
                XAttribute xout = element.Attribute("out");
                if (xout == null || xout.Value == null)
                {
                    Debug.Fail(string.Format("Invalid file format, 'out' attribute is required"));
                    continue;
                }
                _ruleTable.Add(xin.Value[0],xout.Value);
            }

        }
    }
}
