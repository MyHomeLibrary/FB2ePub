using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Media;
using EPubLibrary;
using FB2Library.Elements;
using System.Windows.Media.Imaging;

namespace FB2EPubConverter
{
    internal class ImageManager
    {
        /// <summary>
        /// List of images present in binary form in the FB2 file
        /// </summary>
        private readonly Dictionary<string,bool> presentImagesList = new Dictionary<string,bool>();


        /// <summary>
        /// Get/Set to remove alpha channel from PNG images
        /// </summary>
        public bool RemoveAlpha { get; set; }

        /// <summary>
        /// Registers image ID of the existing image
        /// Should be called for each image stored in binary section
        /// </summary>
        /// <param name="imageId"></param>
        public void RegisterBinaryImageId(string imageId)
        {
            string key = imageId.TrimStart('#');
            presentImagesList.Add(key,false);   
        }

        /// <summary>
        /// Resets the image manager into initial state 
        /// </summary>
        public void Reset()
        {
            presentImagesList.Clear();
        }

        /// <summary>
        /// Load list of the names of "real" images from binary section
        /// </summary>
        /// <param name="binaryImageList">Fb2 binary section</param>
        public void LoadFromBinarySection(Dictionary<string,BinaryItem> binaryImageList)
        {
            presentImagesList.Clear();
            foreach (var binaryItem in binaryImageList)
            {
                string imageName = binaryItem.Value.Id;
                while (presentImagesList.ContainsKey(imageName))
                {
                    imageName = string.Format("_{0}",imageName);
                }
                if (binaryItem.Value.Id != imageName)
                {
                    binaryImageList.Remove(binaryItem.Value.Id);
                    binaryItem.Value.Id = imageName;
                    binaryImageList.Add(imageName,binaryItem.Value);
                }
                presentImagesList.Add(imageName,false);
            }
        }

        /// <summary>
        /// Checks if ImageManager has real (binary)
        /// images defined 
        /// </summary>
        /// <returns>true if binary data at least for some images exists, false otherwise</returns>
        public bool HasRealImages()
        {
            return (presentImagesList.Count > 0);
        }


        /// <summary>
        /// Checks if image Id is one in the list of real (binary) image Ids
        /// </summary>
        /// <param name="imageId">image ID to check</param>
        /// <returns>true it in list, false otherwise</returns>
        public bool IsImageIdReal(string imageId)
        {
            if (presentImagesList.ContainsKey(imageId.TrimStart('#')))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Marks image ID as being used
        /// In case we detected the use of image ID (refference to it)
        /// we mark it used so later it can be saved to file
        /// in case the Id is not among "real" (binary stored) images we just ignore it
        /// </summary>
        /// <param name="imageId"></param>
        public void ImageIdUsed(string imageId)
        {
            string key = imageId.TrimStart('#');
            if (presentImagesList.ContainsKey(key))
            {
                presentImagesList[key] = true;
            }
        }

        /// <summary>
        /// Check if image ID is registered as used anywhere in file
        /// </summary>
        /// <param name="imageId">image id</param>
        /// <returns></returns>
        private bool IsImageIdUsed(string imageId)
        {
            string key = imageId.TrimStart('#');
            if (presentImagesList.ContainsKey(key))
            {
                return presentImagesList[key];
            }
            return false;
        }

        /// <summary>
        /// Converts list of FB2 images to EPub images
        /// </summary>
        /// <param name="fb2Images"></param>
        /// <param name="epubImages"></param>
        public void ConvertFb2ToEpubImages(Dictionary<string,BinaryItem> fb2Images,Dictionary<string,EPUBImage> epubImages)
        {
            epubImages.Clear();
            foreach (var item in fb2Images)
            {
                EPUBImage image = new EPUBImage();
                image.ID = item.Value.Id;
                Logger.log.DebugFormat("Image ID : {0}", image.ID);
                image.ImageData = item.Value.BinaryData;
                if (item.Value.ContentType == ContentTypeEnum.ContentTypeJpeg)
                {
                    image.ImageType = EPUBImageTypeEnum.ImageJPG;
                }
                else if (item.Value.ContentType == ContentTypeEnum.ContentTypePng)
                {
                    image.ImageType = EPUBImageTypeEnum.ImagePNG;
                }
                else if (item.Value.ContentType == ContentTypeEnum.ContentTypeGif)
                {
                    image.ImageType = EPUBImageTypeEnum.ImageGIF;
                }
                else
                {
                    Logger.log.ErrorFormat("Unknown image type {0}", item.Value.ContentType);
                    continue;
                }
                if (IsImageIdUsed(image.ID)) // only if image regestered as used
                {
                    if (RemoveAlpha && (image.ImageType == EPUBImageTypeEnum.ImagePNG))
                    {
                        image.ImageData = GetPNGWithoutAlpha(image.ImageData);
                    }
                    epubImages.Add(string.Format("#{0}", image.ID), image);
                }
                
            }
        }

        private byte[] GetPNGWithoutAlpha(byte[] bytes)
        {
            using (MemoryStream pngStream = new MemoryStream(bytes))
            {
                PngBitmapDecoder decoder = new PngBitmapDecoder(pngStream,BitmapCreateOptions.PreservePixelFormat,BitmapCacheOption.Default);
                if (!AlphaPalette(decoder.Frames[0].Palette))
                {
                    return bytes;
                }
                PngBitmapEncoder encoder = new PngBitmapEncoder();

                FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();

                newFormatedBitmapSource.BeginInit();
                newFormatedBitmapSource.Source = decoder.Frames[0];
                newFormatedBitmapSource.DestinationFormat = PixelFormats.Pbgra32;
                newFormatedBitmapSource.EndInit();
             
                encoder.Frames.Add(BitmapFrame.Create(newFormatedBitmapSource));
                encoder.Interlace = PngInterlaceOption.Off;
                using (MemoryStream outStream = new MemoryStream())
                {
                    encoder.Save(outStream);
                    return outStream.ToArray();
                }
            }
        }



        private static bool AlphaPalette(BitmapPalette palette)
        {
            if (palette == null)
            {
                return false;
            }
            foreach (var color in palette.Colors)
            {
                if ( color.ScA != 0.0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
