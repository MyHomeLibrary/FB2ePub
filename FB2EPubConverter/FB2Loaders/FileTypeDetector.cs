using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Fb2ePubConverter;

namespace FB2EPubConverter.FB2Loaders
{
    /// <summary>
    /// Used for detection of file types
    /// </summary>
    internal class FileTypeDetector
    {
        /// <summary>
        /// Represent acceptable input file types
        /// </summary>
        internal enum FileTypesEnum
        {
            FileTypeZIP,
            FileTypeFB2,
            FileTypeRAR,
            FileTypeUnknown,
        }

        /// <summary>
        /// Detects file type of the input file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static FileTypesEnum DetectFileType(string fileName)
        {
            Logger.Log.DebugFormat("Detecting file type for {0}", fileName);
            var extension = Path.GetExtension(fileName);
            if (extension != null)
                switch (extension.ToUpper())
                {
                    case ".FB2":
                        Logger.Log.Debug("The file is FB2 file");
                        return FileTypesEnum.FileTypeFB2;
                    case ".ZIP":
                        Logger.Log.Debug("The file is ZIP file");
                        return FileTypesEnum.FileTypeZIP;
                    case ".RAR":
                        Logger.Log.Debug("The file is RAR file");
                        return FileTypesEnum.FileTypeRAR;
                    default:
                        Logger.Log.Debug("Can't use extension - attempting to detect");
                        if (FB2ZipFileLoader.IsZipFile(fileName))
                        {
                            Logger.Log.Debug("The file is ZIP file");
                            return FileTypesEnum.FileTypeZIP;
                        }
                        if (FB2RarLoader.IsRarFile(fileName))
                        {
                            Logger.Log.Debug("The file is RAR file");
                            return FileTypesEnum.FileTypeRAR;
                        }
                        if (FB2FileLoader.IsFB2File(fileName))
                        {
                            Logger.Log.Debug("The file is FB2 file");
                            return FileTypesEnum.FileTypeFB2;
                        }
                        break;
                }
            Logger.Log.Debug("The file is unknown file type");
            return FileTypesEnum.FileTypeUnknown;
        }

    }
}
