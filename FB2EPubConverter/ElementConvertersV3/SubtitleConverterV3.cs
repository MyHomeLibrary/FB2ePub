using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class SubtitleConverterParamsV3
    {
        public ConverterOptionsV3 Settings { get; set; }
    }


    internal class SubtitleConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts FB2 subtitle element into XHTML representation
        /// </summary>
        /// <param name="subtitleItem">item to convert</param>
        /// <param name="subtitleConverterParams"></param>
        /// <returns>XHTML representation</returns>
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
            SetClassType(internalData, "subtitle");
            //subtitle.Add(internalData);

            //SetClassType(subtitle);
            //return subtitle;
            return internalData;
        }

    }
}
