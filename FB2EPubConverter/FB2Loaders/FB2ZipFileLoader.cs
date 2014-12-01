using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Fb2ePubConverter;
using Fb2epubSettings;
using FB2Library;
using ICSharpCode.SharpZipLib.Zip;

namespace FB2EPubConverter.FB2Loaders
{
    internal class FB2ZipFileLoader : BaseFB2Loader
    {
        public override List<FB2File> LoadFile(string fileName, FB2ImportSettings settings)
        {
            Logger.Log.DebugFormat("Starting to load ZIP file {0}", fileName);
            var result = new List<FB2File>();
            try
            {
                Exception returnEx = null;
                using (var s = new ZipInputStream(File.OpenRead(fileName)))
                {
                    try
                    {
                        ZipEntry theEntry;
                        while ((theEntry = s.GetNextEntry()) != null)
                        {
                            if (!theEntry.IsFile || !theEntry.CanDecompress)
                            {
                                Logger.Log.InfoFormat("{0} is not file or not decompressable", fileName);
                                continue;
                            }
                            Logger.Log.InfoFormat("Processing {0} ...", theEntry.Name);
                            var extension = Path.GetExtension(theEntry.Name);
                            if (extension != null && extension.ToUpper() == ".FB2")
                            {
                                if (settings.FixMode == FixOptions.Fb2FixAlways)
                                {
                                    using (var s1 = new ZipInputStream(File.OpenRead(fileName)))
                                    {
                                        // reach the same position in ZIP
                                        while (theEntry.ToString() != s1.GetNextEntry().ToString())
                                        {
                                        }
                                        result.Add(LoadFB2StreamWithFix(s1, ReadFb2FileStream));
                                    }
                                }
                                try
                                {
                                    result.Add(ReadFb2FileStream(s));
                                }
                                catch (XmlException) // broken/malformed Xml detected
                                {
                                    if (settings.FixMode == FixOptions.DoNotFix)
                                    {
                                        Logger.Log.ErrorFormat("Error in file, not fixing ");
                                        continue;
                                    }
                                    // try to run work around case 
                                    Logger.Log.Info("Error loading file - invalid XML content - attempting to repair...");
                                    using (var s1 = new ZipInputStream(File.OpenRead(fileName)))
                                    {
                                        // reach the same position in ZIP
                                        while (theEntry.ToString() != s1.GetNextEntry().ToString())
                                        {
                                        }
                                        // try to read broken XML
                                        try
                                        {
                                            result.Add(ReadBrokenXmlFb2FileStream(s1));
                                        }
                                        catch (XmlException)
                                        {
                                            if (settings.FixMode == FixOptions.MinimalFix)
                                            {
                                                Logger.Log.ErrorFormat("Error in file, not fixing ");
                                                continue;
                                            }
                                            s1.Close();
                                            using (var s2 = new ZipInputStream(File.OpenRead(fileName)))
                                            {
                                                // reach the same position in ZIP
                                                while (theEntry.ToString() != s2.GetNextEntry().ToString())
                                                {
                                                }

                                                Logger.Log.Info(
                                                    "Repair attempt failed - attempting to repair using Fb2Fix...");
                                                // try to read broken XML
                                                try
                                                {
                                                    try
                                                    {
                                                        result.Add(LoadFB2StreamWithFix(s2, ReadFb2FileStream));
                                                    }
                                                    catch (XmlException)
                                                    {
                                                        Logger.Log.ErrorFormat("Error in file - unable to fix");
                                                        //continue;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Logger.Log.ErrorFormat("Error in file - Fb2Fix crashes - unable to fix. \nError {0}", ex.Message);
                                                    //continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (ZipException ze)
                    {
                        Logger.Log.ErrorFormat("{0} - problem decompressing the file, UnZip error: {1}", fileName, ze.Message);
                        s.Close();
                        returnEx = ze;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.ErrorFormat("{0} - problem decompressing the file, error: {1}", fileName, ex);
                        s.Close();
                        returnEx = ex;
                    }
                    s.Close();
                }
                Logger.Log.DebugFormat("ZIP file {0} loaded successfully", fileName);
                if (returnEx != null)
                {
                    throw returnEx;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading ZIP file {0} : {1}", fileName, ex);
            }
            return result;
        }

        /// <summary>
        /// Checks if input file is a ZIP archive
        /// </summary>
        /// <param name="fileName">file to check</param>
        /// <returns>true if file is ZIP archive, false otherwise</returns>
        public static bool IsZipFile(string fileName)
        {
            try
            {
                using (new ZipFile(fileName))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}
