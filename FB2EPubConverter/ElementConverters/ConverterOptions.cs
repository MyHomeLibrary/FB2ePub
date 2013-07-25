using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB2EPubConverter.ElementConverters
{
    internal class ConverterOptions
    {
        public HRefManager ReferencesManager { get; set; }
        public ImageManager Images { get; set; }
        public bool CapitalDrop { get; set; }
        public long MaxSize;
        public bool FixCodeSpaces;

    }
}
