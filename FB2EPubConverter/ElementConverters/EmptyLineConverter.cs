using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class EmptyLineConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts empty line FB2 element 
        /// </summary>
        /// <returns>XHTML representation</returns>
        public IXHTMLItem Convert()
        {
            Paragraph el = new Paragraph();
            el.Add(new SimpleEPubText { Text = "\u00A0" });
            SetClassType(el);
            return el;
        }

        public override string GetElementType()
        {
            return "empty-line";
        }
    }
}
