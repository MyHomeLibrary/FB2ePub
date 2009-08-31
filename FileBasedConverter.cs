using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly Dictionary<char,string> ruleTable = new Dictionary<char, string>();
        private string fileName;

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                ruleTable.Clear();
            }
        }


        public override string ConvertCharacter(char inputCharacter)
        {
            if (ruleTable.Count == 0)
            {
                LoadRuleFile();
            }
            // temporary
            if (ruleTable.ContainsKey(inputCharacter))
            {
                return ruleTable[inputCharacter];
            }
            return inputCharacter.ToString();
        }

        private void LoadRuleFile()
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new NullReferenceException("Please set file name first");
            }
            if (!File.Exists(fileName))
            {
                throw new Exception(string.Format("File {0} does not exists on disk",Path.GetFullPath(fileName)));
            }

            using (Stream stream = File.OpenRead(fileName))
            {
                XmlReaderSettings settings = new XmlReaderSettings
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

            ruleTable.Clear();
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
                ruleTable.Add(xin.Value[0],xout.Value);
            }

        }
    }
}
