using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class SubtitleConverterParams
    {
        public ConverterOptions Settings { get; set; }  
    }

    internal class SubtitleConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 subtitle element into XHTML representation
        /// </summary>
        /// <param name="subtitleItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="subtitleConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(SubTitleItem subtitleItem,HTMLElementType compatibility,SubtitleConverterParams subtitleConverterParams)
        {
            if (subtitleItem == null)
            {
                throw new ArgumentNullException("subtitleItem");
            }
            //Div subtitle = new Div();
            var paragraphConverter = new ParagraphConverter();
            var internalData = paragraphConverter.Convert(subtitleItem,compatibility,
                new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, Settings = subtitleConverterParams.Settings, StartSection = false });
            SetClassType(internalData,"subtitle");
            //subtitle.Add(internalData);

            //SetClassType(subtitle);
            //return subtitle;
            return internalData;
        }
    }
}
