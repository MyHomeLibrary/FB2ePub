using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class InlineImageConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }
    }

    internal class InlineImageConverterV2 : BaseElementConverterV2
    {

        /// <summary>
        /// Converts FB2 inline image
        /// </summary>
        /// <returns></returns>
        public IHTMLItem Convert(InlineImageItem inlineImageItem,HTMLElementType compatibility,InlineImageConverterParamsV2 inlineImageConverterParams)
        {
            if (inlineImageItem == null)
            {
                throw new ArgumentNullException("inlineImageItem");
            }
            var img = new Image(compatibility);
            if (inlineImageItem.AltText != null)
            {
                img.Alt.Value = inlineImageItem.AltText;
            }

            img.Source.Value = inlineImageConverterParams.Settings.ReferencesManager.AddImageRefferenced(inlineImageItem, img);

            SetClassType(img,"int_image");
            return img;
        }
    }
}
