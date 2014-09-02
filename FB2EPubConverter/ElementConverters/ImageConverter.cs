using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class ImageConverterParams
    {
        public ConverterOptions Settings { get; set; }
    }

    internal class ImageConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert FB2 image item
        /// </summary>
        /// <param name="imageItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="imageConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ImageItem imageItem,HTMLElementType compatibility,ImageConverterParams imageConverterParams)
        {
            if (imageItem == null)
            {
                throw new ArgumentNullException("imageItem");
            }
            var image = new Image(compatibility);
            if (imageItem.AltText != null)
            {
                image.Alt.Value = imageItem.AltText;
            }
            image.Source.Value = imageConverterParams.Settings.ReferencesManager.AddImageRefferenced(imageItem, image);

            image.GlobalAttributes.ID.Value = imageConverterParams.Settings.ReferencesManager.AddIdUsed(imageItem.ID, image);
            if (imageItem.Title != null)
            {
                image.GlobalAttributes.Title.Value = imageItem.Title;
            }
            imageConverterParams.Settings.Images.ImageIdUsed(imageItem.HRef);
            return image;
        }
    }
}
