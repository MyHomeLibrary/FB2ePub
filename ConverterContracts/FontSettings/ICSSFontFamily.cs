using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterContracts.FontSettings
{
    public interface ICSSFontFamily
    {
        void CopyFrom(ICSSFontFamily cssFontFamily);
        string Name { get; set; }
        List<ICSSFont> Fonts { get; }
    }
}
