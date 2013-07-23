using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EPubLibrary.PathUtils
{
    class FontOnStorage : IEPubPath
    {
        private readonly EPubInternalPath _pathInEPub = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/fonts");
        private readonly string _externalPathToFont = string.Empty;
        private readonly string _mediaType = "application/x-font-ttf";

        public string FileName {
            get { return Path.GetFileName(_externalPathToFont); }
        }

        public string MediaType { get { return _mediaType; }}

        public  string ID {
            get { return Path.GetFileNameWithoutExtension(_externalPathToFont); }
        }

        public FontOnStorage(string externalPathToFont, string mediaType)
        {
            _externalPathToFont = externalPathToFont;
            _mediaType = mediaType;
        }

        public EPubInternalPath PathInEPUB
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
