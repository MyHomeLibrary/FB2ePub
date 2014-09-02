﻿using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class EmptyLineConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts empty line FB2 element 
        /// </summary>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert(HTMLElementType compatibility)
        {
            var el = new Paragraph(compatibility);
            el.Add(new SimpleHTML5Text(compatibility) { Text = "\u00A0" });
            SetClassType(el,"empty-line");
            return el;
        }
    }
}
