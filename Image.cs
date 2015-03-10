using System;
using EPubLibrary.Content;
using EPubLibraryContracts;

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

        public static EPubCoreMediaType ConvertImageTypeToMediaType(EPUBImageTypeEnum imageType)
        {
            switch (imageType)
            {
                case EPUBImageTypeEnum.ImageGIF:
                    return EPubCoreMediaType.ImageGif;
                case EPUBImageTypeEnum.ImageJPG:
                    return EPubCoreMediaType.ImageJpeg;
                case EPUBImageTypeEnum.ImagePNG:
                    return EPubCoreMediaType.ImagePng;
                case EPUBImageTypeEnum.ImageSVG:
                    return EPubCoreMediaType.ImageSvgXml;
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
