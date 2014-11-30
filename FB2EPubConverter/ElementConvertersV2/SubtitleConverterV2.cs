using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class SubtitleConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }  
    }

    internal class SubtitleConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts FB2 subtitle element into XHTML representation
        /// </summary>
        /// <param name="subtitleItem">item to convert</param>
        /// <param name="subtitleConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(SubTitleItem subtitleItem,SubtitleConverterParamsV2 subtitleConverterParams)
        {
            if (subtitleItem == null)
            {
                throw new ArgumentNullException("subtitleItem");
            }
            //Div subtitle = new Div();
            var paragraphConverter = new ParagraphConverterV2();
            var internalData = paragraphConverter.Convert(subtitleItem,
                new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, Settings = subtitleConverterParams.Settings, StartSection = false });
            SetClassType(internalData,"subtitle");
            //subtitle.Add(internalData);

            //SetClassType(subtitle);
            //return subtitle;
            return internalData;
        }
    }
}
