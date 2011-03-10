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
        public ConverterSettings Settings { get; set; }

        abstract public  string GetElementType();

        protected void SetClassType(IBlockElement item)
        {
            item.Class.Value = GetElementType();
        }
    }
}
