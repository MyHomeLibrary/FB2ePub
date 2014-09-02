using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class EmptyLineConverter : BaseElementConverter
    {
        /// <summary>
        /// Converts empty line FB2 element 
        /// </summary>
        /// <returns>XHTML representation</returns>
        public IHTMLItem Convert()
        {
            var el = new Paragraph();
            el.Add(new SimpleHTML5Text { Text = "\u00A0" });
            SetClassType(el);
            return el;
        }

        public override string GetElementType()
        {
            return "empty-line";
        }
    }
}
