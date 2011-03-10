using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHTMLClassLibrary.BaseElements;

namespace FB2EPubConverter.ElementConverters
{
    internal class EmptyLineConverter : BaseElementConverter
    {
        public IXHTMLItem Convert()
        {
            Paragraph el = new Paragraph();
            el.Add(new SimpleEPubText { Text = "\u00A0" });
            el.Class.Value = "empty-line";
            return el;
        }
    }
}
