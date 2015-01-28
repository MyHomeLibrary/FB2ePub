using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterContracts.FontSettings
{
    public interface ICSSStylableElement
    {
        string Name { get; set; }
        string Class { get; set; }
        List<string> AssignedFontFamilies { get; }
        void CopyFrom(ICSSStylableElement element);

    }
}
