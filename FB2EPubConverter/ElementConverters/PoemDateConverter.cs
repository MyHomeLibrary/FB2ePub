using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB2Library.Elements;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConverters
{
    class PoemDateConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts poemdate FB2 element 
        /// </summary>
        /// <param name="poemDateItem">item to convert </param>
        /// <returns>XHTML representation</returns>
        public IXHTMLItem Convert(DateItem poemDateItem)
        {
            if (poemDateItem == null)
            {
                throw new ArgumentNullException("poemDateItem");
            }
            Paragraph date = new Paragraph();
            date.Add(new SimpleEPubText { Text = poemDateItem.Text });
            SetClassType(date);
            return date;
        }

        public override string GetElementType()
        {
            return "poemdate";
        }
    }
}
