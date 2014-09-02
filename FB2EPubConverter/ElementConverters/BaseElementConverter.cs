using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XHTMLClassLibrary.BaseElements;
using XHTMLClassLibrary.BaseElements.BlockElements;

namespace FB2EPubConverter.ElementConverters
{

    internal abstract class  BaseElementConverter
    {
        public ConverterOptions Settings { get; set; }

        abstract public  string GetElementType();

        protected void SetClassType(HTMLItem item)
        {
            item.GlobalAttributes.Class.Value = GetElementType();
        }
    }
}
