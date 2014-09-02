using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class EpigraphAuthorConverter : BaseElementConverter
    {
        /// <summary>
        /// Convert epigrah author FB2 element
        /// </summary>
        /// <param name="textAuthorItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(TextAuthorItem textAuthorItem)
        {
            if (textAuthorItem == null)
            {
                throw new ArgumentNullException("textAuthorItem");
            }
            var epigraphAuthor = new Div();
            var paragraphConverter = new ParagraphConverter {Settings = Settings};
            epigraphAuthor.Add(paragraphConverter.Convert(textAuthorItem, ParagraphConvTargetEnum.Paragraph));
            epigraphAuthor.GlobalAttributes.Class.Value = "epigraph_author";
            return epigraphAuthor;
        }

        public override string GetElementType()
        {
            return  "epigraph_author";
        }
    }
}
