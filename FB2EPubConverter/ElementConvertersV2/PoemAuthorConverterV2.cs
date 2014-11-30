using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class PoemAuthorConverterParamsV2
    {
        public ConverterOptionsV2 Settings { get; set; }  
    }

    internal class PoemAuthorConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts FB2 poem  author element
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="poemAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ParagraphItem paragraphItem,PoemAuthorConverterParamsV2 poemAuthorConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }

            var paragraphConverter = new ParagraphConverterV2();
            var item = paragraphConverter.Convert(paragraphItem,  new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, StartSection = false, Settings = poemAuthorConverterParams .Settings});

            SetClassType(item,"poem_author");
            return item;
        }
    }
}
