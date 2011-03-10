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
        public ImageItem Item { get; set; }

        public IXHTMLItem Convert()
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Image image = new Image();
            if (Item.AltText != null)
            {
                image.Alt.Value = Item.AltText;
            }
            image.Source.Value = Settings.ReferencesManager.AddImageRefferenced(Item, image);

            image.ID.Value = Settings.ReferencesManager.AddIdUsed(Item.ID, image);
            if (Item.Title != null)
            {
                image.Title.Value = Item.Title;
            }
            Settings.Images.ImageIdUsed(Item.HRef);
            return image;
        }

    }
}
