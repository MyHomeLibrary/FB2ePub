using System;

namespace EPubLibrary
{
    public enum EPUBImageTypeEnum
    {
        ImageJPG,
        ImagePNG,
        ImageGIF,
        ImageSVG
    }
    public class EPUBImage
    {
        public byte[] ImageData { get; set; }
        public string ID { get; set; }
        public EPUBImageTypeEnum ImageType { get; set; }

        public static string ConvertImageTypeToMediaType(EPUBImageTypeEnum imageType)
        {
            switch (imageType)
            {
                case EPUBImageTypeEnum.ImageGIF:
                    return "image/gif";
                case EPUBImageTypeEnum.ImageJPG:
                    return "image/jpeg";
                case EPUBImageTypeEnum.ImagePNG:
                    return "image/png";
                case EPUBImageTypeEnum.ImageSVG:
                    return "image/svg+xml";
            }
            throw new Exception("unknown image type passed");
        }

        public static string GetExtensionByMediaType(EPUBImageTypeEnum imageType)
        {
            switch (imageType)
            {
                case EPUBImageTypeEnum.ImageGIF:
                    return "gif";
                case EPUBImageTypeEnum.ImageJPG:
                    return "jpeg";
                case EPUBImageTypeEnum.ImagePNG:
                    return "png";
                case EPUBImageTypeEnum.ImageSVG:
                    return "svg";
            }
            throw new Exception("unknown image type passed");
        }
    }
}
