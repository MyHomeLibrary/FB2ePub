using System;
using ConverterContracts.ConversionElementsStyles;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3.Poem
{
    internal class PoemDateConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts poemdate FB2 element 
        /// </summary>
        /// <param name="poemDateItem">item to convert </param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(DateItem poemDateItem)
        {
            if (poemDateItem == null)
            {
                throw new ArgumentNullException("poemDateItem");
            }
            var date = new Paragraph(HTMLElementType.HTML5);
            date.Add(new SimpleHTML5Text(HTMLElementType.HTML5) { Text = poemDateItem.Text });
            SetClassType(date, ElementStylesV3.PoemDate);
            return date;
        }
    }
}
