using FB2Library.Elements;
using System;
using ConverterContracts.ConversionElementsStyles;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.InlineElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class InlineImageConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
    }


    internal class InlineImageConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts FB2 inline image
        /// </summary>
        /// <returns></returns>
        public IHTMLItem Convert(InlineImageItem inlineImageItem, InlineImageConverterParamsV3 inlineImageConverterParams)
        {
            if (inlineImageItem == null)
            {
                throw new ArgumentNullException("inlineImageItem");
            }
            var img = new Image(HTMLElementType.HTML5);
            img.Alt.Value = inlineImageItem.AltText ?? string.Empty;

            img.Source.Value = inlineImageConverterParams.Settings.ReferencesManager.AddImageRefferenced(inlineImageItem, img);

            SetClassType(img, ElementStylesV3.InlineImage);
            return img;
        }
    }
}
