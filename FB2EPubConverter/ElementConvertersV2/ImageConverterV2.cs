using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class ImageConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }
    }

    internal class ImageConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Convert FB2 image item
        /// </summary>
        /// <param name="imageItem">item to convert</param>
        /// <param name="imageConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ImageItem imageItem,ImageConverterParamsV2 imageConverterParams)
        {
            if (imageItem == null)
            {
                throw new ArgumentNullException("imageItem");
            }
            var image = new Image(HTMLElementType.XHTML11);
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
