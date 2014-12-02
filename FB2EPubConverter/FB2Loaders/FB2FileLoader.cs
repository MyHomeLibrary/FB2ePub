using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Fb2ePubConverter;
using Fb2epubSettings;
using FB2Library;

namespace FB2EPubConverter.FB2Loaders
{
    /// <summary>
    /// Used to load FB2 files
    /// </summary>
    internal class FB2FileLoader : BaseFB2Loader
    {
        public override List<FB2File> LoadFile(string fileName, FB2ImportSettings settings)
        {
            var result = new List<FB2File>();
            try
            {
                using (Stream s = File.OpenRead(fileName))
                {

                    if (settings.FixMode == FixOptions.Fb2FixAlways)
                    {
                        result.Add(LoadFB2StreamWithFix(s, ReadFb2FileStream));
                    }
                    else
                    {
                        try
                        {
                            try
                            {
                                result.Add(ReadFb2FileStream(s));
                            }
                            catch (XmlException)
                            {
                                if (settings.FixMode == FixOptions.DoNotFix)
                                {
                                    Logger.Log.ErrorFormat("Error in file, not fixing ");
                                    return null;
                                }
                                Logger.Log.Info("Error loading file - invalid XML content - attempting to repair...");
                                // try to read broken XML
                                s.Seek(0, SeekOrigin.Begin);
                                result.Add(ReadBrokenXmlFb2FileStream(s));
                            }
                        }
                        catch (XmlException)
                        {
                            if (settings.FixMode == FixOptions.MinimalFix)
                            {
                                Logger.Log.ErrorFormat("Error in file, not fixing ");
                                return result;
                            }
                            Logger.Log.Info("Repair attempt failed - attempting to repair using Fb2Fix...");
                            // try to read broken XML
                            s.Seek(0, SeekOrigin.Begin);
                            result.Add(LoadFB2StreamWithFix(s, ReadFb2FileStream));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading FB2 file : {0}", ex);
            }
            return result;
        }

        public static bool IsFB2File(string fileName)
        {
            using (var s = File.OpenRead(fileName))
            {
                var settings = new XmlReaderSettings
                {
                    ValidationType = ValidationType.None,
                    DtdProcessing = DtdProcessing.Prohibit,
                    CheckCharacters = false

                };
                try
                {
                    using (XmlReader reader = XmlReader.Create(s, settings))
                    {
                        XNamespace fb2Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0";
                        XDocument fb2Document = XDocument.Load(reader, LoadOptions.PreserveWhitespace);
                        if (fb2Document.Root != null &&
                            fb2Document.Root.Name.LocalName == "FictionBook" &&
                            fb2Document.Root.Name.Namespace == fb2Namespace)
                        {
                            reader.Close();
                            return true;
                        }
                        reader.Close();
                    }

                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

    }
}
