using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    class PoemDateConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts poemdate FB2 element 
        /// </summary>
        /// <param name="poemDateItem">item to convert </param>
        /// <param name="compatibility"></param>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(DateItem poemDateItem,HTMLElementType compatibility)
        {
            if (poemDateItem == null)
            {
                throw new ArgumentNullException("poemDateItem");
            }
            var date = new Paragraph(compatibility);
            date.Add(new SimpleHTML5Text(compatibility) { Text = poemDateItem.Text });
            SetClassType(date,"poemdate");
            return date;
        }
    }
}
