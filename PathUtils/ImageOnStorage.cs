using System;
using EPubLibraryContracts;

namespace EPubLibrary.PathUtils
{
    public class ImageOnStorage : IEPubPath
    {
        public static readonly EPubInternalPath DefaultImagesStoragePath = new EPubInternalPath(EPubInternalPath.DefaultOebpsFolder + "/images/");
        private readonly EPubInternalPath _pathInEPub = DefaultImagesStoragePath;
        private readonly string _id;
        private readonly EPUBImageTypeEnum _imageType;

        public string FileName { get; set; }

        public ImageOnStorage(EPUBImage eImage)
        {
            _id = eImage.ID;
            _imageType = eImage.ImageType;
        }

        public string ID {
            get { return _id; }
        }

        public IEPubInternalPath PathInEPUB
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
