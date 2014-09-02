using System;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    class PoemDateConverter : BaseElementConverter
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
            var date = new Paragraph();
            date.Add(new SimpleHTML5Text { Text = poemDateItem.Text });
            SetClassType(date);
            return date;
        }

        public override string GetElementType()
        {
            return "poemdate";
        }
    }
}
