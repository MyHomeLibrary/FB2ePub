using ConverterContracts.ConversionElementsStyles;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV3
{
    internal class EmptyLineConverterV3 : BaseElementConverterV3
    {
        /// <summary>
        /// Converts empty line FB2 element 
        /// </summary>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert()
        {
            var el = new Paragraph(HTMLElementType.HTML5);
            el.Add(new SimpleHTML5Text(HTMLElementType.HTML5) { Text = "\u00A0" });
            SetClassType(el, ElementStylesV3.EmptyLine);
            return el;
        }

    }
}
