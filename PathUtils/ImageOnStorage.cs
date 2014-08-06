using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPubLibrary.PathUtils
{
    public class ImageOnStorage : IEPubPath
    {
        public static readonly EPubInternalPath DefaultImagesStoragePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/images/");
        private readonly EPubInternalPath _pathInEPub = DefaultImagesStoragePath;
        private readonly string _id = string.Empty;
        private readonly EPUBImageTypeEnum _imageType = EPUBImageTypeEnum.ImageJPG;

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
