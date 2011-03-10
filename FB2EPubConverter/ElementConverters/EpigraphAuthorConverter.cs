using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
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
        public IBlockElement Convert(TextAuthorItem textAuthorItem)
        {
            if (textAuthorItem == null)
            {
                throw new ArgumentNullException("textAuthorItem");
            }
            Div epigraphAuthor = new Div();
            ParagraphConverter paragraphConverter = new ParagraphConverter {Settings = Settings};
            epigraphAuthor.Add(paragraphConverter.Convert(textAuthorItem, ParagraphConvTargetEnum.Paragraph));
            epigraphAuthor.Class.Value = "epigraph_author";
            return epigraphAuthor;
        }

        public override string GetElementType()
        {
            return  "epigraph_author";
        }
    }
}
