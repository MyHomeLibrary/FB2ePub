using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class EpigraphAuthorConverterParams
    {
        public ConverterOptions Settings { get; set; }  
    }

    internal class EpigraphAuthorConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert epigrah author FB2 element
        /// </summary>
        /// <param name="textAuthorItem">item to convert</param>
        /// <param name="compatibility"></param>
        /// <param name="epigraphAuthorConverterParams"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(TextAuthorItem textAuthorItem,HTMLElementType compatibility,EpigraphAuthorConverterParams epigraphAuthorConverterParams)
        {
            if (textAuthorItem == null)
            {
                throw new ArgumentNullException("textAuthorItem");
            }
            var epigraphAuthor = new Div(compatibility);
            var paragraphConverter = new ParagraphConverter();
            epigraphAuthor.Add(paragraphConverter.Convert(textAuthorItem,compatibility,
                new ParagraphConverterParams { ResultType = ParagraphConvTargetEnum.Paragraph, Settings = epigraphAuthorConverterParams.Settings, StartSection = false }));
            SetClassType(epigraphAuthor,"epigraph_author");
            return epigraphAuthor;
        }
    }
}
