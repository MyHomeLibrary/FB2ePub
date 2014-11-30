using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConvertersV2
{
    internal class EmptyLineConverterV2 : BaseElementConverterV2
    {
        /// <summary>
        /// Converts empty line FB2 element 
        /// </summary>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert()
        {
            var el = new Paragraph(HTMLElementType.XHTML11);
            el.Add(new SimpleHTML5Text(HTMLElementType.XHTML11) { Text = "\u00A0" });
            SetClassType(el,"empty-line");
            return el;
        }
    }
}
