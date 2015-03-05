using System;
using ConverterContracts.ConversionElementsStyles;
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
        public IHTMLItem Convert(InlineImageItem inlineImageItem,InlineImageConverterParamsV2 inlineImageConverterParams)
        {
            if (inlineImageItem == null)
            {
                throw new ArgumentNullException("inlineImageItem");
            }
            var img = new Image(HTMLElementType.XHTML11);
            img.Alt.Value = inlineImageItem.AltText ?? string.Empty; // ePub require image always to have attribute

            img.Source.Value = inlineImageConverterParams.Settings.ReferencesManager.AddImageRefferenced(inlineImageItem, img);

            SetClassType(img, ElementStylesV2.InlineImage);
            return img;
        }
    }
}
