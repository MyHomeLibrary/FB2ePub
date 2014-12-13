using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EPubLibrary.Content;
using EPubLibrary.CSS_Items;
using EPubLibrary.PathUtils;
using ICSharpCode.SharpZipLib.Zip;

namespace EPubLibrary.Template
{
    /// <summary>
    /// Contains code for loading and saving adobe XPGT templates
    /// </summary>
    public class AdobeTemplate : StyleElement
    {
        internal static class Logger
        {
            // Create a logger for use in this class
            public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

        }

        private readonly EPubInternalPath _pathInEPub = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/template/");


        private  XDocument _fileDocument = null;


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

        public void Write(ZipOutputStream stream)
        {
            if (_fileDocument == null)
            {
                throw new NullReferenceException("Document pointer is null - file need to be properly loaded first");
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            using (var writer = XmlWriter.Create(stream, settings))
            {
                _fileDocument.WriteTo(writer);
            }
            
        }

        public override void Write(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override EPubInternalPath PathInEPUB
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


        public override EPubCoreMediaType GetMediaType()
        {
            return EPubCoreMediaType.AdditionalAddobeTemplateXml;
        }
    }
}
