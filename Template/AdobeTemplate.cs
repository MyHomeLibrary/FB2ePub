using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Content;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using ICSharpCode.SharpZipLib.Zip;
using EPubLibraryContracts;

namespace EPubLibrary.Template
{
    /// <summary>
    /// Contains code for loading and saving adobe XPGT templates
    /// </summary>
    public class AdobeTemplate : IStyleElement
    {
        private readonly EPubInternalPath _pathInEPub = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/template/");


        private  XDocument _fileDocument;


        public string FileName { get { return "template.xpgt"; } }

        /// <summary>
        /// Full path and file name to the template file
        /// </summary>
        public string TemplateFileInputPath { get; set; }

        public string ID
        {
            get { return Path.GetFileNameWithoutExtension(TemplateFileInputPath); }
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(TemplateFileInputPath))
            {
                throw new NullReferenceException("Input path is null");
            }
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings
                {
                    ValidationType = ValidationType.None,
                    DtdProcessing = DtdProcessing.Prohibit,
                    CheckCharacters = false

                };
                using (XmlReader reader = XmlReader.Create(TemplateFileInputPath, settings))
                {
                    _fileDocument = XDocument.Load(reader, LoadOptions.PreserveWhitespace);
                    reader.Close();
                }

            }
            catch (XmlException ex) // we handle this on top
            {
                Logger.Log.ErrorFormat("The template file {0} contains invalid XML, error: {1}", TemplateFileInputPath, ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}", ex);
                throw;
            }

        }

    
        public void Write(Stream stream)
        {
            if (_fileDocument == null)
            {
                throw new NullReferenceException("Document pointer is null - file need to be properly loaded first");
            }
            XmlWriterSettings settings = new XmlWriterSettings
            {
                CloseOutput = false,
                Encoding = Encoding.UTF8,
                Indent = true
            };
            using (var writer = XmlWriter.Create(stream, settings))
            {
                _fileDocument.WriteTo(writer);
            }
        }

        public IEPubInternalPath PathInEPUB
        {
            get
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    throw new NullReferenceException("FileName property has to be set");
                }
                return new EPubInternalPath(_pathInEPub, FileName);
            }
        }


        public EPubCoreMediaType GetMediaType()
        {
            return EPubCoreMediaType.AdditionalAddobeTemplateXml;
        }
    }
}
