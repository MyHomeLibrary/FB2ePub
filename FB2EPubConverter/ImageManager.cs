using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly Dictionary<string,bool> _presentImagesList = new Dictionary<string,bool>();


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
            _presentImagesList.Add(key,false);   
        }


        /// <summary>
        /// Removes image ID of the existing image
        /// in case image invalid etc
        /// </summary>
        /// <param name="imageId"></param>
        public void RemoveBinaryImageId(string imageId)
        {
            string key = imageId.TrimStart('#');
            if (_presentImagesList.ContainsKey(key))
            {
                _presentImagesList.Remove(key);                
            }
        }


        /// <summary>
        /// Resets the image manager into initial state 
        /// </summary>
        public void Reset()
        {
            _presentImagesList.Clear();
        }

        /// <summary>
        /// Load list of the names of "real" images from binary section
        /// </summary>
        /// <param name="binaryImageList">Fb2 binary section</param>
        public void LoadFromBinarySection(Dictionary<string,BinaryItem> binaryImageList)
        {
            _presentImagesList.Clear();
            foreach (var binaryItem in binaryImageList)
            {
                string imageName = binaryItem.Value.Id;
                while (_presentImagesList.ContainsKey(imageName))
                {
                    imageName = string.Format("_{0}",imageName);
                }
                if (binaryItem.Value.Id != imageName)
                {
                    binaryImageList.Remove(binaryItem.Value.Id);
                    binaryItem.Value.Id = imageName;
                    binaryImageList.Add(imageName,binaryItem.Value);
                }
                _presentImagesList.Add(imageName,false);
            }
        }

        /// <summary>
        /// Checks if ImageManager has real (binary)
        /// images defined 
        /// </summary>
        /// <returns>true if binary data at least for some images exists, false otherwise</returns>
        public bool HasRealImages()
        {
            return (_presentImagesList.Count > 0);
        }


        /// <summary>
        /// Checks if image Id is one in the list of real (binary) image Ids
        /// </summary>
        /// <param name="imageId">image ID to check</param>
        /// <returns>true it in list, false otherwise</returns>
        public bool IsImageIdReal(string imageId)
        {
            if (_presentImagesList.ContainsKey(imageId.TrimStart('#')))
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
            if (_presentImagesList.ContainsKey(key))
            {
                _presentImagesList[key] = true;
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
            if (_presentImagesList.ContainsKey(key))
            {
                return _presentImagesList[key];
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
                var image = new EPUBImage {ID = item.Value.Id};
                Logger.Log.DebugFormat("Image ID : {0}", image.ID);
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
                    Logger.Log.ErrorFormat("Unknown image type {0}", item.Value.ContentType);
                    continue;
                }
                if (IsImageIdUsed(image.ID)) // only if image registered as used
                {
                    try
                    {

                        if (RemoveAlpha && (image.ImageType == EPUBImageTypeEnum.ImagePNG))
                        {
                            image.ImageData = GetPNGWithoutAlpha(image.ImageData);
                        }
                        epubImages.Add(string.Format("#{0}", image.ID), image);
                    }
                    catch (Exception ex)
                    {
                        RemoveBinaryImageId(image.ID);
                        Logger.Log.ErrorFormat("Error reading PNG file {0}",ex);
                    }
                }               
            }
        }

        private byte[] GetPNGWithoutAlpha(byte[] bytes)
        {
            using (var pngStream = new MemoryStream(bytes))
            {
                var decoder = new PngBitmapDecoder(pngStream,BitmapCreateOptions.PreservePixelFormat,BitmapCacheOption.Default);
                if (!AlphaPalette(decoder.Frames[0].Palette))
                {
                    return bytes;
                }
                var encoder = new PngBitmapEncoder();

                var newFormatedBitmapSource = new FormatConvertedBitmap();

                newFormatedBitmapSource.BeginInit();
                newFormatedBitmapSource.Source = decoder.Frames[0];
                newFormatedBitmapSource.DestinationFormat = PixelFormats.Pbgra32;
                newFormatedBitmapSource.EndInit();
             
                encoder.Frames.Add(BitmapFrame.Create(newFormatedBitmapSource));
                encoder.Interlace = PngInterlaceOption.Off;
                using (var outStream = new MemoryStream())
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
            return palette.Colors.Any(color => Math.Abs(color.ScA) > float.Epsilon);
        }
    }
}
