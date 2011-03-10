using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public IXHTMLItem Convert(ImageItem imageItem)
        {
            if (imageItem == null)
            {
                throw new ArgumentNullException("imageItem");
            }
            Image image = new Image();
            if (imageItem.AltText != null)
            {
                image.Alt.Value = imageItem.AltText;
            }
            image.Source.Value = Settings.ReferencesManager.AddImageRefferenced(imageItem, image);

            image.ID.Value = Settings.ReferencesManager.AddIdUsed(imageItem.ID, image);
            if (imageItem.Title != null)
            {
                image.Title.Value = imageItem.Title;
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
