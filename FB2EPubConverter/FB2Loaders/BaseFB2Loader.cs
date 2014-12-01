using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Fb2ePubConverter;
using Fb2epubSettings;
using FB2Fix;
using FB2Library;
using XMLFixerLibrary;

namespace FB2EPubConverter.FB2Loaders
{
    internal abstract class BaseFB2Loader : IFB2Loader
    {
        public abstract List<FB2File> LoadFile(string fileName, FB2ImportSettings settings);

        protected FB2File ReadFb2FileStream(Stream s)
        {
            Logger.Log.Debug("Starting to load FB2 stream");
            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.None,
                DtdProcessing = DtdProcessing.Prohibit,
                CheckCharacters = false

            };
            XDocument fb2Document;
            try
            {
                using (XmlReader reader = XmlReader.Create(s, settings))
                {
                    fb2Document = XDocument.Load(reader, LoadOptions.PreserveWhitespace);
                    reader.Close();
                }

            }
            catch (XmlException) // we handle this on top
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}", ex);
                throw;
            }

            var file = new FB2File();
            try
            {
                file.Load(fb2Document, false);
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}", ex);
                throw;
            }
            Logger.Log.Debug("FB2 stream loaded");
            return file;
        }

        protected FB2File LoadFB2StreamWithFix(Stream s, Func<Stream,FB2File> streamLoader)
        {
            var options = new Fb2FixArguments
            {
                incversion = true,
                regenerateId = false,
                indentBody = false,
                indentHeader = true,
                mapGenres = true,
                encoding = Encoding.UTF8
            };

            using (Stream output = Fb2Fix.Process(s, options))
            {
                return streamLoader(output);
            }
        }

        protected FB2File ReadBrokenXmlFb2FileStream(Stream stream)
        {
            Logger.Log.Debug("Starting to load FB2 stream");
            try
            {
                using (var ms = new MemoryStream())
                {
                    var fixer = new XmlRepair();
                    fixer.Repair(stream, ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return ReadFb2FileStream(ms);
                }

            }
            catch (XmlException xex)
            {
                Logger.Log.WarnFormat("Error loading file - invalid XML content : {0} \nRepair attempt failed", xex);
                throw;

            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading file : {0}", ex);
                throw;
            }
        }



    }
}
