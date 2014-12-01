using System;
using System.Collections.Generic;
using System.IO;
using Fb2ePubConverter;
using Fb2epubSettings;
using FB2Library;
using NUnrar.Archive;

namespace FB2EPubConverter.FB2Loaders
{
    internal class FB2RarLoader : BaseFB2Loader
    {
        public override List<FB2File> LoadFile(string fileName, FB2ImportSettings settings)
        {
            var result = new List<FB2File>();
            try
            {
                RarArchive rarFile = RarArchive.Open(fileName);

                int n = rarFile.Entries.Count;
                Logger.Log.DebugFormat("Detected {0} entries in RAR file", n);
                foreach (var entry in rarFile.Entries)
                {
                    if (entry.IsDirectory)
                    {
                        Logger.Log.DebugFormat("{0} is not file but folder", fileName);
                        continue;
                    }
                    var extension = Path.GetExtension(entry.FilePath);
                    if (extension != null && extension.ToUpper() == ".FB2")
                    {
                        try
                        {
                            string tempPath = Path.GetTempPath();
                            entry.WriteToDirectory(tempPath);
                            string fileNameOnly = Path.GetFileName(entry.FilePath);
                            Logger.Log.InfoFormat("Processing {0} ...", fileNameOnly);
                            var fb2Loader = new FB2FileLoader();
                            try
                            {
                                result.AddRange(fb2Loader.LoadFile(string.Format(@"{0}\{1}", tempPath, fileNameOnly),settings));
                            }
                            catch (Exception)
                            {
                                Logger.Log.ErrorFormat("Unable to load {0}", fileNameOnly);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Log.ErrorFormat("Unable to unrar file entry {0} : {1}", entry.FilePath, ex);
                        }
                    }
                    else
                    {
                        Logger.Log.InfoFormat("{0} is not FB2 file", entry.FilePath);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("Error loading RAR file : {0}", ex);
            }
            return result;
         
        }

        public static bool IsRarFile(string fileName)
        {
            try
            {
                return RarArchive.IsRarFile(fileName);
            }
            catch (Exception ex)
            {
                Logger.Log.Debug(ex.Message);
            }
            return false;
        }


    }
}
