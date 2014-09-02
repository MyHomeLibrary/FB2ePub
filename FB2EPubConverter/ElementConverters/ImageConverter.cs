using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class ImageConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert FB2 image item
        /// </summary>
        /// <param name="imageItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ImageItem imageItem)
        {
            if (imageItem == null)
            {
                throw new ArgumentNullException("imageItem");
            }
            var image = new Image();
            if (imageItem.AltText != null)
            {
                image.Alt.Value = imageItem.AltText;
            }
            image.Source.Value = Settings.ReferencesManager.AddImageRefferenced(imageItem, image);

            image.GlobalAttributes.ID.Value = Settings.ReferencesManager.AddIdUsed(imageItem.ID, image);
            if (imageItem.Title != null)
            {
                image.GlobalAttributes.Title.Value = imageItem.Title;
            }
            Settings.Images.ImageIdUsed(imageItem.HRef);
            return image;
        }

        public override string GetElementType()
        {
            return string.Empty;
        }
    }
}
