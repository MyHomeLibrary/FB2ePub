using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace EPubLibrary.Template
{
    /// <summary>
    /// Contains code for loading and saving adobe XPGT templates
    /// </summary>
    internal class AdobeTemplate : StyleElement
    {
        internal static class Logger
        {
            // Create a logger for use in this class
            public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());

        }

        /// <summary>
        /// Get default CSS media type
        /// </summary>
        public static string MediaType { get { return @"application/adobe-page-template+xml"; } }

        private  XDocument fileDocument = null;

        /// <summary>
        /// Full path and file name to the template file
        /// </summary>
        public string TemplateFileInputPath { get; set; }

        /// <summary>
        /// Returns internal name of the file (without path)
        /// </summary>
        public string TemplateFileOutputName
        {
            get { return string.IsNullOrEmpty(TemplateFileInputPath) ? "template.xpgt" : Path.GetFileName(TemplateFileInputPath); }
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
                    ProhibitDtd = true,
                    CheckCharacters = false

                };
                using (XmlReader reader = XmlReader.Create(TemplateFileInputPath, settings))
                {
                    fileDocument = XDocument.Load(reader, LoadOptions.PreserveWhitespace);
                    reader.Close();
                }

            }
            catch (XmlException ex) // we handle this on top
            {
                Logger.Log.ErrorFormat("The template file {0} contains invalid XML, error: {1}", TemplateFileInputPath, ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}", ex.ToString());
                throw;
            }

        }

        public void Write(ZipOutputStream stream)
        {
            if (fileDocument == null)
            {
                throw new NullReferenceException("Document pointer is null - file need to be properly loaded first");
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = false;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            using (var writer = XmlWriter.Create(stream, settings))
            {
                fileDocument.WriteTo(writer);
            }
            
        }

        public override void Write(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override string GetFilePathExt()
        {
            return TemplateFileOutputName;
        }

        public override string GetMediaType()
        {
            return MediaType;
        }
    }
}
