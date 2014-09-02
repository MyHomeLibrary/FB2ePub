using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class PoemAuthorConverterParams
    {
        public ConverterOptions Settings { get; set; }  
    }

    internal class PoemAuthorConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 poem  author element
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="poemAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(ParagraphItem paragraphItem,HTMLElementType compatibility,PoemAuthorConverterParams poemAuthorConverterParams)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }

            var paragraphConverter = new ParagraphConverter();
            var item = paragraphConverter.Convert(paragraphItem, compatibility, new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, StartSection = false, Settings = poemAuthorConverterParams .Settings});

            SetClassType(item,"poem_author");
            return item;
        }
    }
}
