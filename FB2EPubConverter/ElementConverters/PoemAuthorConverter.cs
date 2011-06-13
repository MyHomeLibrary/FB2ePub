using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class PoemAuthorConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts FB2 poem  author element
        /// </summary>
        /// <param name="paragraphItem">item to convert</param>
        /// <returns>XHTML representation</returns>
        public IXHTMLItem Convert(ParagraphItem paragraphItem)
        {
            if (paragraphItem == null)
            {
                throw new ArgumentNullException("paragraphItem");
            }

            ParagraphConverter paragraphConverter = new ParagraphConverter { Settings = Settings };
            IBlockElement item = paragraphConverter.Convert(paragraphItem, ParagraphConvTargetEnum.Paragraph);

            SetClassType(item);
            return item;
        }

        public override string GetElementType()
        {
            return "poem_author";
        }
    }
}
