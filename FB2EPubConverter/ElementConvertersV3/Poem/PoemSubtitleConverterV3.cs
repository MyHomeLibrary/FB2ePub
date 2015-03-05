using System;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV3.Poem
{
    internal class PoemSubtitleConverterV3 : BaseElementConverterV3
    {
        public IHTMLItem Convert(SubTitleItem subtitleItem, SubtitleConverterParamsV3 subtitleConverterParams)
        {
            if (subtitleItem == null)
            {
                throw new ArgumentNullException("subtitleItem");
            }
            //Div subtitle = new Div();
            var paragraphConverter = new ParagraphConverterV3();
            var internalData = paragraphConverter.Convert(subtitleItem,
                new ParagraphConverterParamsV3 { ResultType = ParagraphConvTargetEnumV3.Paragraph, Settings = subtitleConverterParams.Settings, StartSection = false });
            SetClassType(internalData, ElementStylesV3.PoemSubTitle);
            //subtitle.Add(internalData);

            //SetClassType(subtitle);
            //return subtitle;
            return internalData;
        }

    }
}
