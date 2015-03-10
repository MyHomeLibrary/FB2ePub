using System;
using System.IO;
using EPubLibrary.Content;
using EPubLibraryContracts;

namespace EPubLibrary.PathUtils
{
    public class FontOnStorage : IEPubPath
    {
        private readonly EPubInternalPath _pathInEPub = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/fonts");
        private readonly string _externalPathToFont;
        private readonly EPubCoreMediaType _mediaType;

        public string FileName {
            get { return Path.GetFileName(_externalPathToFont); }
        }

        public EPubCoreMediaType MediaType { get { return _mediaType; } }

        public  string ID {
            get { return Path.GetFileNameWithoutExtension(_externalPathToFont); }
        }

        public FontOnStorage(string externalPathToFont, EPubCoreMediaType mediaType)
        {
            _externalPathToFont = externalPathToFont;
            _mediaType = mediaType;
        }

        public IEPubInternalPath PathInEPUB
        {
            get
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    throw new NullReferenceException("FileName property can't be empty");
                }
                return new EPubInternalPath(_pathInEPub, FileName);
            }
        }
    }
}
