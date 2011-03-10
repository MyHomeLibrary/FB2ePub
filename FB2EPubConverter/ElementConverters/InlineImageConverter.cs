using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class InlineImageConverter : BaseElementConverter
    {
        public InlineImageItem Item { get; set; }

        /// <summary>
        /// Converts FB2 inline image
        /// </summary>
        /// <returns></returns>
        public IXHTMLItem Convert()
        {
            if (Item == null)
            {
                throw new NullReferenceException("Item");
            }
            Image img = new Image();
            if (Item.AltText != null)
            {
                img.Alt.Value = Item.AltText;
            }

            img.Source.Value = Settings.ReferencesManager.AddImageRefferenced(Item, img);

            img.Class.Value = "int_image";
            return img;
        }

    }
}
