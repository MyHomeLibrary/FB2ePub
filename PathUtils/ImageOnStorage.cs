using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPubLibrary.PathUtils
{
    class ImageOnStorage : IEPubPath
    {
        private readonly EPubInternalPath _pathInEPub = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/images");
        private string _id = string.Empty;
        private EPUBImageTypeEnum _imageType = EPUBImageTypeEnum.ImageJPG;

        public string FileName { get; set; }

        public ImageOnStorage(EPUBImage eImage)
        {
            _id = eImage.ID;
            _imageType = eImage.ImageType;
        }

        public string ID {
            get { return _id; }
        }

        public EPubInternalPath PathInEPUB
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

        public  EPUBImageTypeEnum ImageType { get { return _imageType; }}
    }
}
