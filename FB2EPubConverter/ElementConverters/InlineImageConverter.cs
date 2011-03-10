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

        /// <summary>
        /// Converts FB2 inline image
        /// </summary>
        /// <returns></returns>
        public IXHTMLItem Convert(InlineImageItem inlineImageItem)
        {
            if (inlineImageItem == null)
            {
                throw new ArgumentNullException("inlineImageItem");
            }
            Image img = new Image();
            if (inlineImageItem.AltText != null)
            {
                img.Alt.Value = inlineImageItem.AltText;
            }

            img.Source.Value = Settings.ReferencesManager.AddImageRefferenced(inlineImageItem, img);

            img.Class.Value = "int_image";
            return img;
        }

        public override string GetElementType()
        {
            return  "int_image";
        }
    }
}
